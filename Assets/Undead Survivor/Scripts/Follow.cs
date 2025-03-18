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
        // ���� ��ǥ�� ��ũ�� ��ǥ�� �ٸ��� ������ �̷��� �ҷ����� �ȵȴ�
        // rect.position = GameManager.instance.player.transform.position;
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
