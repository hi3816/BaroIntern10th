using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    //프리팹을 보관
    public GameObject[] prefabs;
    public int poolSize;

    // 풀을 담당
    List<GameObject>[] pools;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) 
            {
                // ... 발견하면 모두 할당
                select = item;
                select.SetActive(true);
                return select;
            }
        }


        return select;
    }

}
