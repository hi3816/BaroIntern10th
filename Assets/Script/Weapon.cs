using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    // Update is called once per frame

    private void Start()
    {
        Init();
    }
    void Update()
    {
        
    }

    public void Init()
    {
        switch (id) 
        {
            case 0:
                speed = -150;
                Batch();

                break;
            default:
                break;
        }
            
    }

    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = GameManager.instance.poolManager.Get(prefabId);
            bullet.SetActive(true);

            bullet.transform.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per.
        }
    }
}
