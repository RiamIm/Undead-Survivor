using UnityEngine;

public class Reposition : MonoBehaviour
{
    public int repositionValue;

    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {   
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position; // 플레이어 위치
        Vector3 myPos = transform.position; // 현재 위치(타일)
        float diffX = Mathf.Abs(playerPos.x - myPos.x); // 절댓값으로 플레이어 위치와 타일 위치의 차이(거리)를 구함
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // input System 을 사용하면 대각선으로 이동할때 normalized 을 했으면 -1, 1 값이 아니라 0.7 같은 애매한 값이 나오기 때문에
        // 0보다 작으면 -1, 0보다 크면 1 로 초기화 해줘야함
        Vector3 playerDir = GameManager.instance.player.inputVector; // inputVector 플레이어 이동 방향을 가져옴
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY) // 가로축 변화가 더 클때, 맵이 가로로 이동
                {
                    transform.Translate(dirX * 40, 0, 0);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(0, dirY * 40, 0);
                }
                else
                {
                    transform.Translate(dirX * 40, dirY * 40, 0);
                }
                    break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * repositionValue + new Vector3(Random.Range(-.3f, 3f), Random.Range(-.3f, 3f), 0));
                }
                break;
            default:
                break;
        }
    }
}
