using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVector;
    // �÷��̾� ��ũ��Ʈ���� �˻� Ŭ���� Ÿ�� ���� ���� �� �ʱ�ȭ
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
        // 3. ��ġ �̵�
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
