using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;



public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;

    //gpt
    public float attackRange = 2.0f;
    public float attackCooldown = 1.0f;
    private float lastAttackTime = 0f;

    private void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPlayer(this); 
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void DetectEnemies()
    {
        float detectionRadius = 10.0f; // 감지 반경 설정
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("적 감지됨: " + enemy.name);
        }
    }

    // Gizmos로 감지 범위 표시 (디버깅 용)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return; // 쿨타임 적용

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(10); // 적에게 데미지 주기
            Debug.Log(enemy.name + "에게 공격!");
        }

        lastAttackTime = Time.time;
    }

}
