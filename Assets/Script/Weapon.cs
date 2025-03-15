using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
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
                timer += Time.deltaTime;

                if (timer > speed)
                { 
                    timer = 0f;
                    Fire();
                }
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
                speed = 0.3f;
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
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget) 
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = PoolManager.Instance.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
