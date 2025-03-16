using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTable;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private List<item> _itemDataList;
    private List<monster> _monsterDataList;


    void Awake()
    {
        // ½Ì±ÛÅæ ¼³Á¤
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¾À ÀüÈ¯ ½Ã¿¡µµ À¯Áö
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadItemData();
        LoadMonsterData();
    }

    private void LoadItemData()
    {
        _itemDataList = item.GetList();
    }

    private void LoadMonsterData()
    {
        _monsterDataList = monster.GetList();
    }

    public List<monster> GetMosnterData()
    {
        return _monsterDataList;
    }
    public List<item> GetItemData()
    {
        return _itemDataList;
    }
}
