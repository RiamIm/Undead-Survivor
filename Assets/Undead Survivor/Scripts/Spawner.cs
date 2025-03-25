using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �ڽ� ������Ʈ�� Ʈ�������� ���� �迭 ���� ����
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!GameManager.instance.bLive)
        {
            return;
        }

        timer += Time.deltaTime;
        // �Ҽ��� �Ʒ��� ������ int ������ �ٲٴ� �Լ�
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // GetComponentsInChildren �� �ڱ��ڽŵ� �����̱� ������ �迭�� ������ �ڽ� ������Ʈ�� ���õǵ��� �ҷ��� 1 ���� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// ����ȭ: ��ü�� ���� Ȥ�� �����ϱ� ���� ��ȯ
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
