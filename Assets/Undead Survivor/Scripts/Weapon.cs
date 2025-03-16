using UnityEngine;

// poolManager ���� �޾ƿ� ����, �Ѿ� ���� ��� �ȿ��� ���� �ְ� �������ִ� ��ũ��Ʈ
public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    private void Start()
    {
        Init();
    }
    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150; 
                Batch();
                break;
            default:
                break;
        }
    }

    // ������ ���⸦ ��ġ�ϴ� �Լ� ����
    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; // �θ� �ڱ� �ڽ����� ����
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVector = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 �� ������ ����
        }
    }
}
