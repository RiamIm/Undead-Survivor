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

        Vector3 playerPos = GameManager.instance.player.transform.position; // �÷��̾� ��ġ
        Vector3 myPos = transform.position; // ���� ��ġ(Ÿ��)
        float diffX = Mathf.Abs(playerPos.x - myPos.x); // �������� �÷��̾� ��ġ�� Ÿ�� ��ġ�� ����(�Ÿ�)�� ����
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // input System �� ����ϸ� �밢������ �̵��Ҷ� normalized �� ������ -1, 1 ���� �ƴ϶� 0.7 ���� �ָ��� ���� ������ ������
        // 0���� ������ -1, 0���� ũ�� 1 �� �ʱ�ȭ �������
        Vector3 playerDir = GameManager.instance.player.inputVector; // inputVector �÷��̾� �̵� ������ ������
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY) // ������ ��ȭ�� �� Ŭ��, ���� ���η� �̵�
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
