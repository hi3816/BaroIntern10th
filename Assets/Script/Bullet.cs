using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per; //관통

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }    
}
