using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // �ݶ��̴��� ������ ���� �̸� ����

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // �ݶ��̴� ������Ʈ ��������
        dashCollider.enabled = false; // ������ �� �ݶ��̴� ��Ȱ��ȭ
    }

    void Update()
    {
        // ���� ������ ���� �ݶ��̴� Ȱ��ȭ
        dashCollider.enabled = controller.isDashing; 
    }

    // �浹 ó�� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (controller.isDashing) // ��Ʈ�ѷ��� isDashing ���� Ȯ��
        {
            ApplyDashDamage(collision.collider);
        }
    }

    private void ApplyDashDamage(Collider2D collider)
    {
        EnemyMove enemyMove = collider.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(controller.dashDamage); // �뽬 ������ ����
        }

        BossHP boss = collider.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(controller.dashDamage); // �������� �뽬 ������ ����
        }

        RangedMonster rangedMonster = collider.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(controller.dashDamage); // ���Ÿ� ���Ϳ��� �뽬 ������ ����
        }
    }

    // Ʈ���� �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other);
            controller.currentdashcooldown = 0; // �뽬 ��Ÿ�� �ʱ�ȭ
        }
        else if (other.CompareTag("Boss")) 
        {
            HandleBossCollision(other);
        }
    }

    private void HandleEnemyCollision(Collider2D other)
    {
        EnemyMove enemyMove = other.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(controller.damage); 
            Debug.Log("���� ����; ����: " + controller.attackDirection); 

            ReduceDashCooldown(); // �뽬 ��Ÿ�� ����
        }

        RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(controller.damage); 
            Debug.Log("���� ����; ����: " + controller.attackDirection); 

            ReduceDashCooldown(); // �뽬 ��Ÿ�� ����
        }
    }

    private void HandleBossCollision(Collider2D other)
    {
        BossHP boss = other.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(controller.damage);
            Debug.Log("���� ���� ����; ����: " + controller.attackDirection); 

            ReduceDashCooldown(); // �뽬 ��Ÿ�� ����
        }
    }

    private void ReduceDashCooldown()
    {
        if (!controller.cooldownReduced)
        {
            controller.currentdashcooldown -= 1f;
            if (controller.currentdashcooldown < 0)
            {
                controller.currentdashcooldown = 0;
            }
            controller.cooldownReduced = true; // ��ٿ� ���Ұ� �� ���� �Ͼ���� ����
        }
    }
}
