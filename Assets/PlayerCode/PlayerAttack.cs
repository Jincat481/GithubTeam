using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // �ִϸ����� ������Ʈ
    [SerializeField] private float meleeSpeed; // ���� �ӵ�
    [SerializeField] private float damage = 1f; // ���ݷ�
    [SerializeField] private Collider2D attackCollider; // ���� ���� ��� �ݶ��̴�

    private float timeUntilMelee; // ���� ��Ÿ��
    private Vector2 attackDirection; // ���� ����
    private bool attackProcessed; // ������ ó���Ǿ����� ���θ� ��Ÿ���� �÷���

    private void Update()
    {
        // ���콺 ��Ŭ�� �Է��� �����Ͽ� ���� ����
        if (Input.GetMouseButtonDown(0)) // 0�� ��Ŭ���� �ǹ���
        {
            PerformAttack();
        }

        // ���� ��Ÿ�� ����
        if (timeUntilMelee > 0f)
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    public void PerformAttack()
    {
        if (timeUntilMelee <= 0f)
        {
            // ���콺 ��Ŭ�� �� ����
            if (Input.GetMouseButtonDown(0))
            {
                // ���콺 ��ġ�� ���� ���� ���� ����
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                attackDirection = (mousePosition - (Vector2)transform.position).normalized;

                anim.SetTrigger("Attack"); // �ִϸ��̼� Ʈ���� ȣ��
                timeUntilMelee = meleeSpeed; // ��Ÿ�� �ʱ�ȭ
                attackProcessed = false; // ������ ó������ �ʾ����� ǥ��
            }
        }
        else
        {
            timeUntilMelee -= Time.deltaTime; // ��Ÿ�� ����
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    private void ApplyDamage()
    {
        if (attackProcessed)
        {
            return; // ������ �̹� ó���� ��� �� �̻� �������� ����
        }

        // ���� ���� ���� ��� �ݶ��̴��� ������
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0f);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = hitCollider.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.TakeDamage(damage);
                    Debug.Log("���� ����; ����: " + attackDirection);
                }

                RangedMonster rangerMonster = hitCollider.GetComponent<RangedMonster>();
                if (rangerMonster != null)
                {
                    rangerMonster.TakeDamage(damage);
                    Debug.Log("���� ����; ����: " + attackDirection);
                }
            }
            else if (hitCollider.CompareTag("Boss"))
            {
                BossHP boss = hitCollider.GetComponent<BossHP>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                    Debug.Log("���� ����; ����: " + attackDirection);
                }
            }
        }

        attackProcessed = true; // ������ ó���Ǿ����� ǥ��
    }

    private void OnDrawGizmos()
    {
        // ����׿� ���� �ݶ��̴� �ð�ȭ
        if (attackCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);
        }
    }
}
