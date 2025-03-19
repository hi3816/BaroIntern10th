using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance { get; private set; }

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
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
