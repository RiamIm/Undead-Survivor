using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        // 월드 좌표와 스크린 좌표는 다르기 때문에 이렇게 불러오면 안된다
        // rect.position = GameManager.instance.player.transform.position;
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
