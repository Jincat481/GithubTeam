using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // �ִϸ����� ������Ʈ
    [SerializeField] private float meleeSpeed = 0.5f; // ���� �ӵ�
    [SerializeField] private float damage = 1f; // ���� ������
    [SerializeField] private Collider2D attackCollider; // ���� �ݶ��̴� (Is Trigger�� ����)
    [SerializeField] private PlayerController playerController; // PlayerController ����

    private float timeUntilMelee; // ���� ��Ÿ��

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
            // ���� �ִϸ��̼� Ʈ���� ȣ��
            anim.SetTrigger("Attack");

            // ���� ó�� �ڷ�ƾ ȣ��
            StartCoroutine(AttackCoroutine());

            // ��Ÿ�� �ʱ�ȭ
            timeUntilMelee = meleeSpeed;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // ���� �ִϸ��̼��� ���̿� ���� ���
        float attackAnimationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // ���� �ݶ��̴� Ȱ��ȭ
        attackCollider.enabled = true;

        // �ִϸ��̼� ���� ���� ���
        yield return new WaitForSeconds(attackAnimationLength);

        // ���� �ݶ��̴� ��Ȱ��ȭ
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���ŵ� ������Ʈ�� ���� ��� ������ ����
        bool damageApplied = false; // ������ ���� ���θ� Ȯ���ϱ� ���� ����

        if (other.CompareTag("Enemy"))
        {
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            if (enemyMove != null)
            {
                enemyMove.TakeDamage(damage); // ������ ����
                Debug.Log("������ ���� ����");
                damageApplied = true;
            }

            RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
            if (rangedMonster != null)
            {
                rangedMonster.TakeDamage(damage); // ������ ����
                Debug.Log("���Ÿ� ���Ϳ��� ���� ����");
                damageApplied = true;
            }
        }
        else if (other.CompareTag("Boss"))
        {
            BossHP boss = other.GetComponent<BossHP>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // ������ ����
                Debug.Log("�������� ���� ����");
                damageApplied = true;
            }
        }

        // �������� ����Ǿ��� ��� �뽬 ��Ÿ�� ����
        if (damageApplied)
        {
            ReduceDashCooldown();
        }
    }

    private void ReduceDashCooldown()
    {
        // ���� �뽬 ��Ÿ�ӿ��� 1�ʸ� ���ҽ�Ŵ
        if (playerController != null && playerController.currentdashcooldown > 0)
        {
            playerController.currentdashcooldown -= 1f;
            Debug.Log("�뽬 ��Ÿ�� 1�� ����");
        }
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
