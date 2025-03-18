using DataTable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Loading;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SlotList : MonoBehaviour
{
    SpriteAtlas SpriteAtlas;
    GameObject slot;
    
    public Image img;
    public string prefabPath = "slot";

    public TextMeshProUGUI testLoadText;


    private void Start()
    {
        SetSlots();
    }

    void SetSlots()
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab != null && this.transform != null)
        {
            List<monster> monsterDataList = DataManager.Instance.GetMosnterData();

            for (int i = 0; i < monsterDataList.Count; i++)
            {
                testLoadText.text += $"{i + 1}. {monsterDataList[i].name}\n"; // 줄바꿈 포함
            }

            //slot생성
            GameObject instance = Instantiate(prefab, this.transform); // 부모를 설정하여 생성
            instance.transform.localPosition = Vector3.zero; // 부모 기준 위치 조정

            //슬롯 텍스트 수정
            TextMeshProUGUI textComponent = instance.GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("프리팹을 찾을 수 없거나 부모 오브젝트가 없습니다.");
        }
    }

    
}
