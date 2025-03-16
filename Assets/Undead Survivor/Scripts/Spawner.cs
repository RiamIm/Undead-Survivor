using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �ڽ� ������Ʈ�� Ʈ�������� ���� �迭 ���� ����
    public Transform[] spwanPoint;

    int level;
    float timer;

    private void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // �Ҽ��� �Ʒ��� ������ int ������ �ٲٴ� �Լ�
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        if (timer > (level == 0 ? 0.5f : 0.2f))
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level);
        // GetComponentsInChildren �� �ڱ��ڽŵ� �����̱� ������ �迭�� ������ �ڽ� ������Ʈ�� ���õǵ��� �ҷ��� 1 ���� ����
        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}
