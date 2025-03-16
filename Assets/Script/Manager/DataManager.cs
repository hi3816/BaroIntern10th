using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private List<Human_Data> _humanDataList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoadBaseMonsterData()
    {
        _baseMonsterDataList = Monster_Data.GetList();
        _baseMonsterDataDictionary = new Dictionary<int, Monster_Data>();

        foreach (var baseMonsterData in _baseMonsterDataList)
        {
            _baseMonsterDataDictionary[baseMonsterData.id] = baseMonsterData;
        }
    }
}
