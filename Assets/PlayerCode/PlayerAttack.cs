using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // 애니메이터 컴포넌트
    [SerializeField] private float meleeSpeed = 0.5f; // 공격 속도
    [SerializeField] private float damage = 1f; // 공격 데미지
    [SerializeField] private Collider2D attackCollider; // 공격 콜라이더 (Is Trigger로 설정)
    [SerializeField] private PlayerController playerController; // PlayerController 참조

    private float timeUntilMelee; // 공격 쿨타임

    private void Update()
    {
        // 마우스 좌클릭 입력을 감지하여 공격 수행
        if (Input.GetMouseButtonDown(0)) // 0은 좌클릭을 의미함
        {
            PerformAttack();
        }

        // 공격 쿨타임 감소
        if (timeUntilMelee > 0f)
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    public void PerformAttack()
    {
        if (timeUntilMelee <= 0f)
        {
            // 공격 애니메이션 트리거 호출
            anim.SetTrigger("Attack");

            // 공격 처리 코루틴 호출
            StartCoroutine(AttackCoroutine());

            // 쿨타임 초기화
            timeUntilMelee = meleeSpeed;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // 공격 애니메이션의 길이에 따라 대기
        float attackAnimationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // 공격 콜라이더 활성화
        attackCollider.enabled = true;

        // 애니메이션 길이 동안 대기
        yield return new WaitForSeconds(attackAnimationLength);

        // 공격 콜라이더 비활성화
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거된 오브젝트가 적인 경우 데미지 적용
        bool damageApplied = false; // 데미지 적용 여부를 확인하기 위한 변수

        if (other.CompareTag("Enemy"))
        {
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            if (enemyMove != null)
            {
                enemyMove.TakeDamage(damage); // 데미지 적용
                Debug.Log("적에게 공격 성공");
                damageApplied = true;
            }

            RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
            if (rangedMonster != null)
            {
                rangedMonster.TakeDamage(damage); // 데미지 적용
                Debug.Log("원거리 몬스터에게 공격 성공");
                damageApplied = true;
            }
        }
        else if (other.CompareTag("Boss"))
        {
            BossHP boss = other.GetComponent<BossHP>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // 데미지 적용
                Debug.Log("보스에게 공격 성공");
                damageApplied = true;
            }
        }

        // 데미지가 적용되었을 경우 대쉬 쿨타임 감소
        if (damageApplied)
        {
            ReduceDashCooldown();
        }
    }

    private void ReduceDashCooldown()
    {
        // 현재 대쉬 쿨타임에서 1초를 감소시킴
        if (playerController != null && playerController.currentdashcooldown > 0)
        {
            playerController.currentdashcooldown -= 1f;
            Debug.Log("대쉬 쿨타임 1초 감소");
        }
    }

    private void OnDrawGizmos()
    {
        // 디버그용 공격 콜라이더 시각화
        if (attackCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);
        }
    }
}
