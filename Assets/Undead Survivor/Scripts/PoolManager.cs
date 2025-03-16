using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ��������� ������ ����
    public GameObject[] prefabs;

    // Ǯ �迭 ����
    List<GameObject>[] pools;

    private void Awake()
    {
        // prefabs�� ���̸�ŭ �迭 �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        // �ݺ����� ���� ��� ������Ʈ Ǯ ����Ʈ�� �ʱ�ȭ
        for (int i = 0; i < pools.Length; ++i)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��� �ִ� ���ӿ�����Ʈ ����
        foreach(var item in pools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            // �� ã������? ���Ӱ� �����ϰ� select ������ �Ҵ�
            // Instantiate: ���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
