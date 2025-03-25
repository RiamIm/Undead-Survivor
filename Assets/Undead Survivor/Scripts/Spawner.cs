using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언
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
        // 소수점 아래는 버리고 int 형으로 바꾸는 함수
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
        // GetComponentsInChildren 은 자기자신도 포함이기 때문에 배열로 했을때 자식 오브젝트만 선택되도록 할려면 1 부터 시작
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 직렬화: 개체를 저장 혹은 전송하기 위해 변환
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
