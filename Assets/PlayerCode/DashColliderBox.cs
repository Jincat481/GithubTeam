using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // �ݶ��̴��� ������ ����
    private PlayerEffects playerEffects; // �÷��̾� ����Ʈ ��ũ��Ʈ

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // �ݶ��̴� ������Ʈ ��������
        dashCollider.enabled = false; // ������ �� �ݶ��̴� ��Ȱ��ȭ

        // PlayerEffects ������Ʈ ��������
        playerEffects = GetComponentInParent<PlayerEffects>();
    }

    void Update()
    {
        // �뽬 ������ ���� �ݶ��̴� Ȱ��ȭ
        dashCollider.enabled = controller.isDashing;
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller.isDashing)
        {
            if (other.CompareTag("Enemy"))
            {
                ApplyDashDamage(other); // �뽬 ����� ����
                controller.currentdashcooldown = 0; // �뽬 ��Ÿ�� �ʱ�ȭ

                // �뽬 ����Ʈ ���
                if (playerEffects != null)
                {
                    playerEffects.PlayDashEffect();
                }

                // �뽬 ���� ���
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.Play("Dash");
                }
            }
            else if (other.CompareTag("Boss"))
            {
                ApplyDashDamage(other); // �뽬 ����� ����

                // �뽬 ����Ʈ ���
                if (playerEffects != null)
                {
                    playerEffects.PlayDashEffect();
                }

                // �뽬 ���� ���
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.Play("Dash");
                }
            }
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
}
