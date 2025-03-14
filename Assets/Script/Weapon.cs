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
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);

                break;
            default:
                break;
        }
    }

    public void Init()
    {
        switch (id) 
        {
            case 0:
                speed = 150;
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
            GameObject bullet = PoolManager.Instance.Get(prefabId);
            bullet.SetActive(true);
            Transform bulletTr = bullet.transform;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bulletTr.Rotate(rotVec);
            bulletTr.Translate(bulletTr.up * 1.5f, Space.World);

            bulletTr.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per.
        }
    }
}
