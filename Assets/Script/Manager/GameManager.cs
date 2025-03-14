using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PoolManager poolManager;
    private Player player;
    private void Awake()
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

    public void RegisterPlayer(Player playerObject)
    {
        player = playerObject;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
