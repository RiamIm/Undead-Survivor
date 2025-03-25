using UnityEngine;
using UnityEngine.U2D.Animation;

public class Hand : MonoBehaviour
{
    public bool bLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0f);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool bReverse = player.flipX;

        if (bLeft)  // 근접
        {
            transform.localRotation = bReverse ? leftRotReverse : leftRot;
            spriter.flipY = bReverse;
            spriter.sortingOrder = bReverse ? 4 : 6;
        }
        else        // 원거리
        {
            transform.localPosition = bReverse ? rightPosReverse : rightPos;
            spriter.flipX = bReverse;
            spriter.sortingOrder = bReverse ? 6 : 4;
        }
    }
}
