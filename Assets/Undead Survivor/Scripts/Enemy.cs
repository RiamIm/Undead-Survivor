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

        Vector2 dirVector = target.position - rigid.position; // 타겟 위치 - 나의 위치 를 통해 방향을 구함 (크기가 1은 아님)
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime; // 그 방향 벡터의 크기를 1로 만들어 주고 speed 와 고정된 시간 간격을 연산을 해줌
        rigid.MovePosition(rigid.position + nextVector); // 그 방향으로 이동하는 로직 (하지만, 순간이동식으로 위치만 바뀜)
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
        // 타겟의 위치가 자신의 위치보다 왼쪽에 있으면 filp
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // OnEnable(): 스크립트가 활성화 될 때, 호출되는 이벤트 함수
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

    // 레벨링에 따른 몬스터의 상태를 직접 컨트롤 할 수 있게 됨
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Bullet 이랑 안 닿았다
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

    // 생명 주기와 비동기처럼 실행되는 함수
    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVector = transform.position - playerPos;
        rigid.AddForce(dirVector.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
