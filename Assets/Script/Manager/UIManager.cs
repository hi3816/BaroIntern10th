using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public float screenWidth = 1920;
    public float screenHeight = 1080;
    private const string _popupPath = "UI/UIPopup/";

    private List<UIBase> uiList = new List<UIBase>();

    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public T Show<T>() where T : UIBase
    {
        string uiName = typeof(T).ToString();
        UIBase go = Resources.Load<UIBase>("UI/" + uiName);
        var ui = Load<T>(go, uiName);
        uiList.Add(ui);
        ui.Opened();

        return (T)ui;
    }

    private T Show<T>(string path) where T : UIBase
    {
        string uiName = typeof(T).ToString();
        UIBase go = Resources.Load<UIBase>(path + uiName);
        var ui = Load<T>(go, uiName);
        uiList.Add(ui);
        ui.Opened();

        return (T)ui;
    }
    public T ShowPopup<T>() where T : UIBase
    {
        return Show<T>(_popupPath);
    }

    private T Load<T>(UIBase prefab, string uiName) where T : UIBase
    {
        GameObject newCanvasObject = new GameObject(uiName + "Canvas");

        var canvas = newCanvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var canvasScaler = newCanvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight);
        //canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 1.0f;

        newCanvasObject.AddComponent<GraphicRaycaster>();

        UIBase ui = Instantiate(prefab, newCanvasObject.transform);
        ui.name = ui.name.Replace("(Clone)", "");
        ui.canvas = canvas;
        ui.canvas.sortingOrder = uiList.Count;

        return (T)ui;
    }

    public void Hide<T>() where T : UIBase
    {
        string uiName = typeof(T).ToString();
        Hide(uiName);
    }

    public void Hide(string uiName)
    {
        UIBase go = uiList.Find(obj => obj.name == uiName);
        uiList.Remove(go);
        Destroy(go.canvas.gameObject);
    }
}