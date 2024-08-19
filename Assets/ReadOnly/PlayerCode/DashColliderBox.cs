using UnityEngine;
using System.Collections.Generic;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // �ݶ��̴��� ������ ����
    private PlayerEffects playerEffects; // �÷��̾� ����Ʈ ��ũ��Ʈ
    private bool effectPlayed = false; // ����Ʈ�� ����Ǿ����� Ȯ���ϴ� �÷���
    private HashSet<Collider2D> hitObjects = new HashSet<Collider2D>(); // �浹 ó���� ������Ʈ ����

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>();
        dashCollider.enabled = false;
        playerEffects = GetComponentInParent<PlayerEffects>(); // PlayerEffects ������Ʈ ��������
    }

    void Update()
    {
        // �뽬 ������ ���� �ݶ��̴� Ȱ��ȭ
        dashCollider.enabled = controller.isDashing;


        if (!controller.isDashing)
        {
            effectPlayed = false;
            hitObjects.Clear();
        }
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller.isDashing && !hitObjects.Contains(other))
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                ApplyDashDamage(other);

                // ������ �ƴ� ��쿡�� �뽬 ��Ÿ�� �ʱ�ȭ
                if (!other.CompareTag("Boss"))
                {
                    controller.currentdashcooldown = 0;
                }

                // �뽬 ����Ʈ ��� (����Ʈ�� �� ���� ����ǵ��� Ȯ��)
                if (!effectPlayed && playerEffects != null)
                {
                    if (controller.dashDirection.x > 0)
                    {
                        playerEffects.PlayRightDashEffect(); // ������ �뽬 ����Ʈ
                    }
                    else
                    {
                        playerEffects.PlayLeftDashEffect(); // ���� �뽬 ����Ʈ
                    }
                    effectPlayed = true;
                }

                // �뽬 ���� ���
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.Play("Dash");
                }


                hitObjects.Add(other);
            }
        }
    }

    private void ApplyDashDamage(Collider2D collider)
    {
        EnemyMove enemyMove = collider.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(controller.dashDamage);
        }

        BossHP boss = collider.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(controller.dashDamage);
        }

        RangedMonster rangedMonster = collider.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(controller.dashDamage);
        }
    }
}
