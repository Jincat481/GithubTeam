using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // 콜라이더를 저장할 변수
    private PlayerEffects playerEffects; // 플레이어 이펙트 스크립트

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // 콜라이더 컴포넌트 가져오기
        dashCollider.enabled = false; // 시작할 때 콜라이더 비활성화

        // PlayerEffects 컴포넌트 가져오기
        playerEffects = GetComponentInParent<PlayerEffects>();
    }

    void Update()
    {
        // 대쉬 상태일 때만 콜라이더 활성화
        dashCollider.enabled = controller.isDashing;
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller.isDashing)
        {
            if (other.CompareTag("Enemy"))
            {
                ApplyDashDamage(other); // 대쉬 대미지 적용
                controller.currentdashcooldown = 0; // 대쉬 쿨타임 초기화

                // 대쉬 이펙트 재생
                if (playerEffects != null)
                {
                    playerEffects.PlayDashEffect();
                }

                // 대쉬 사운드 재생
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.Play("Dash");
                }
            }
            else if (other.CompareTag("Boss"))
            {
                ApplyDashDamage(other); // 대쉬 대미지 적용

                // 대쉬 이펙트 재생
                if (playerEffects != null)
                {
                    playerEffects.PlayDashEffect();
                }

                // 대쉬 사운드 재생
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
            enemyMove.TakeDamage(controller.dashDamage); // 대쉬 데미지 적용
        }

        BossHP boss = collider.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(controller.dashDamage); // 보스에게 대쉬 데미지 적용
        }

        RangedMonster rangedMonster = collider.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(controller.dashDamage); // 원거리 몬스터에게 대쉬 데미지 적용
        }
    }
}
