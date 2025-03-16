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

        Vector2 dirVector = target.position - rigid.position; // 타겟 위치 - 나의 위치 를 통해 방향을 구함 (크기가 1은 아님)
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime; // 그 방향 벡터의 크기를 1로 만들어 주고 speed 와 고정된 시간 간격을 연산을 해줌
        rigid.MovePosition(rigid.position + nextVector); // 그 방향으로 이동하는 로직 (하지만, 순간이동식으로 위치만 바뀜)
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
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
