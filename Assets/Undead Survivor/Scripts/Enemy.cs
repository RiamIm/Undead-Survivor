using System.Collections;
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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.bLive)
        {
            return;
        }

        if (!bLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.instance.bLive)
        {
            return;
        }

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
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !bLive)
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            bLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    // ���� �ֱ�� �񵿱�ó�� ����Ǵ� �Լ�
    IEnumerator KnockBack()
    {
        yield return wait; // ���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVector = transform.position - playerPos;
        rigid.AddForce(dirVector.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
