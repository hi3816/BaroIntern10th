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
                testLoadText.text += $"{i + 1}. {monsterDataList[i].name}\n"; // �ٹٲ� ����
            }

            //slot����
            GameObject instance = Instantiate(prefab, this.transform); // �θ� �����Ͽ� ����
            instance.transform.localPosition = Vector3.zero; // �θ� ���� ��ġ ����

            //���� �ؽ�Ʈ ����
            TextMeshProUGUI textComponent = instance.GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("�������� ã�� �� ���ų� �θ� ������Ʈ�� �����ϴ�.");
        }
    }

    
}
