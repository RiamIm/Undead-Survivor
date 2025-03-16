using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언
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
        // 소수점 아래는 버리고 int 형으로 바꾸는 함수
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
        // GetComponentsInChildren 은 자기자신도 포함이기 때문에 배열로 했을때 자식 오브젝트만 선택되도록 할려면 1 부터 시작
        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}
