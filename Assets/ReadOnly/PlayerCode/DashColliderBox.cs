using UnityEngine;
using System.Collections.Generic;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // 콜라이더를 저장할 변수
    private PlayerEffects playerEffects; // 플레이어 이펙트 스크립트
    private bool effectPlayed = false; // 이펙트가 재생되었는지 확인하는 플래그
    private HashSet<Collider2D> hitObjects = new HashSet<Collider2D>(); // 충돌 처리된 오브젝트 저장

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>();
        dashCollider.enabled = false;
        playerEffects = GetComponentInParent<PlayerEffects>(); // PlayerEffects 컴포넌트 가져오기
    }

    void Update()
    {
        // 대쉬 상태일 때만 콜라이더 활성화
        dashCollider.enabled = controller.isDashing;


        if (!controller.isDashing)
        {
            effectPlayed = false;
            hitObjects.Clear();
        }
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller.isDashing && !hitObjects.Contains(other))
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                ApplyDashDamage(other);

                // 보스가 아닌 경우에만 대쉬 쿨타임 초기화
                if (!other.CompareTag("Boss"))
                {
                    controller.currentdashcooldown = 0;
                }

                // 대쉬 이펙트 재생 (이펙트가 한 번만 재생되도록 확인)
                if (!effectPlayed && playerEffects != null)
                {
                    if (controller.dashDirection.x > 0)
                    {
                        playerEffects.PlayRightDashEffect(); // 오른쪽 대쉬 이펙트
                    }
                    else
                    {
                        playerEffects.PlayLeftDashEffect(); // 왼쪽 대쉬 이펙트
                    }
                    effectPlayed = true;
                }

                // 대쉬 사운드 재생
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
