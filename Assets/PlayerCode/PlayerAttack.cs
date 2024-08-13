using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // �ִϸ����� ������Ʈ
    [SerializeField] private float meleeSpeed; // ���� �ӵ�
    [SerializeField] private Collider2D attackCollider; // ���� ���� ��� �ݶ��̴�

    private float timeUntilMelee; // ���� ��Ÿ��
    private bool attackProcessed; // ������ ó���Ǿ����� ���θ� ��Ÿ���� �÷���

    private PlayerEffects playerEffects; // �÷��̾� ����Ʈ ��ũ��Ʈ

    private void Start()
    {
        // PlayerEffects ������Ʈ ��������
        playerEffects = GetComponent<PlayerEffects>();
    }

    private void Update()
    {
        // ��Ÿ���� �� �Ǿ��ٸ� ���� ����
        if (timeUntilMelee <= 0f)
        {
            // ���콺 ��Ŭ�� �� ����
            if (Input.GetMouseButtonDown(0))
            {
                // ���� �ִϸ��̼� Ʈ���� ȣ��
                anim.SetTrigger("Attack");

                // ���� ó�� �ڷ�ƾ ȣ��
                StartCoroutine(AttackCoroutine());

                // ��Ÿ�� �ʱ�ȭ
                timeUntilMelee = meleeSpeed;
                attackProcessed = false; // ������ ó������ �ʾ����� ǥ��
            }
        }
        else
        {
            // ��Ÿ�� ����
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // ���� �ִϸ��̼��� ���̿� ���� ���
        float attackAnimationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // ���� �ݶ��̴��� �Ѱ� �ִϸ��̼� ���� ���� ����
        attackCollider.enabled = true;
        SoundManager.Instance.Play("Attack"); // ���� ���� ���

        // ���� ����Ʈ ���
        if (playerEffects != null)
        {
            playerEffects.PlayAttackEffect();
        }

        // �ִϸ��̼� ���� ���� ���
        yield return new WaitForSeconds(attackAnimationLength);

        // �ݶ��̴��� ���� ���� ó���� ��ħ
        attackCollider.enabled = false;

        // ���� ó�� �޼��� ȣ��
        ApplyDamage();

        // ������ ó���Ǿ����� ǥ��
        attackProcessed = true;
    }

    private void ApplyDamage()
    {
        // ���� ���� ���� ��� �ݶ��̴��� ������
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.bounds.size.x / 2);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = hitCollider.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.TakeDamage(1f); // ������ 1 ����
                    Debug.Log("������ ���� ����");
                }

                RangedMonster rangedMonster = hitCollider.GetComponent<RangedMonster>();
                if (rangedMonster != null)
                {
                    rangedMonster.TakeDamage(1f); // ������ 1 ����
                    Debug.Log("���Ÿ� ���Ϳ��� ���� ����");
                }
            }
            else if (hitCollider.CompareTag("Boss"))
            {
                BossHP boss = hitCollider.GetComponent<BossHP>();
                if (boss != null)
                {
                    boss.TakeDamage(1f); // ������ 1 ����
                    Debug.Log("�������� ���� ����");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCollider.bounds.center, attackCollider.bounds.size.x / 2);
        }
    }
}
