using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool bLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        bLive = true;
        health = maxHealth;
    }

    // �������� ���� ������ ���¸� ���� ��Ʈ�� �� �� �ְ� ��
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Bullet �̶� �� ��Ҵ�
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            // Live, Hit Action
        }
        else
        {
            // Die
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
