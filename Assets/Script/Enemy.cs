using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] aniController;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rd;
    Animator animator;
    SpriteRenderer spriter;

    public float enemyTime;
    float timer;

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();   
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > enemyTime) 
        {
            timer = 0;
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rd.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
        rd.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < rd.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.Instance.GetPlayer().GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = aniController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health; 
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {

        }
        else
        {
            Dead();
        }

        void Dead() 
        {
            gameObject.SetActive (false);
        }
    }
}
