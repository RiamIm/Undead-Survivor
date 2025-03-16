using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩들을 보관할 변수
    public GameObject[] prefabs;

    // 풀 배열 선언
    List<GameObject>[] pools;

    private void Awake()
    {
        // prefabs의 길이만큼 배열 초기화
        pools = new List<GameObject>[prefabs.Length];

        // 반복문을 통해 모든 오브젝트 풀 리스트를 초기화
        for (int i = 0; i < pools.Length; ++i)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는 게임오브젝트 접근
        foreach(var item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            // 못 찾았으면? 새롭게 생성하고 select 변수에 할당
            // Instantiate: 원본 오브젝트를 복제하여 장면에 생성하는 함수
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
