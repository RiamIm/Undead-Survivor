using UnityEditor.Timeline;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool bLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!bLive)
        {
            return;
        }

        Vector2 dirVector = target.position - rigid.position; // Ÿ�� ��ġ - ���� ��ġ �� ���� ������ ���� (ũ�Ⱑ 1�� �ƴ�)
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime; // �� ���� ������ ũ�⸦ 1�� ����� �ְ� speed �� ������ �ð� ������ ������ ����
        rigid.MovePosition(rigid.position + nextVector); // �� �������� �̵��ϴ� ���� (������, �����̵������� ��ġ�� �ٲ�)
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!bLive)
        {
            return;
        }    
        // Ÿ���� ��ġ�� �ڽ��� ��ġ���� ���ʿ� ������ filp
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // OnEnable(): ��ũ��Ʈ�� Ȱ��ȭ �� ��, ȣ��Ǵ� �̺�Ʈ �Լ�
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
