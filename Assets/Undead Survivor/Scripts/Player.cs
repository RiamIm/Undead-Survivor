using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVector;
    // 플레이어 스크립트에서 검색 클래스 타입 변수 선언 및 초기화
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void FixedUpdate()
    {
        Vector2 nextVector = inputVector * speed * Time.fixedDeltaTime;
        // 3. 위치 이동
        rigid.MovePosition(rigid.position + nextVector);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVector.magnitude);

        if (inputVector.x != 0 )
        {
            spriter.flipX = inputVector.x < 0; 
        }
    }
}
