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
        float detectionRadius = 10.0f; // ���� �ݰ� ����
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("�� ������: " + enemy.name);
        }
    }

    // Gizmos�� ���� ���� ǥ�� (����� ��)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return; // ��Ÿ�� ����

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(10); // ������ ������ �ֱ�
            Debug.Log(enemy.name + "���� ����!");
        }

        lastAttackTime = Time.time;
    }

}
