using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // 애니메이터 컴포넌트
    [SerializeField] private float meleeSpeed; // 공격 속도
    [SerializeField] private Collider2D attackCollider; // 공격 범위 담당 콜라이더

    private float timeUntilMelee; // 공격 쿨타임
    private bool attackProcessed; // 공격이 처리되었는지 여부를 나타내는 플래그

    private PlayerEffects playerEffects; // 플레이어 이펙트 스크립트

    private void Start()
    {
        // PlayerEffects 컴포넌트 가져오기
        playerEffects = GetComponent<PlayerEffects>();
    }

    private void Update()
    {
        // 쿨타임이 다 되었다면 공격 가능
        if (timeUntilMelee <= 0f)
        {
            // 마우스 좌클릭 시 공격
            if (Input.GetMouseButtonDown(0))
            {
                // 공격 애니메이션 트리거 호출
                anim.SetTrigger("Attack");

                // 공격 처리 코루틴 호출
                StartCoroutine(AttackCoroutine());

                // 쿨타임 초기화
                timeUntilMelee = meleeSpeed;
                attackProcessed = false; // 공격이 처리되지 않았음을 표시
            }
        }
        else
        {
            // 쿨타임 감소
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // 공격 애니메이션의 길이에 따라 대기
        float attackAnimationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // 공격 콜라이더를 켜고 애니메이션 길이 동안 유지
        attackCollider.enabled = true;
        SoundManager.Instance.Play("Attack"); // 공격 사운드 재생

        // 공격 이펙트 재생
        if (playerEffects != null)
        {
            playerEffects.PlayAttackEffect();
        }

        // 애니메이션 길이 동안 대기
        yield return new WaitForSeconds(attackAnimationLength);

        // 콜라이더를 끄고 공격 처리를 마침
        attackCollider.enabled = false;

        // 공격 처리 메서드 호출
        ApplyDamage();

        // 공격이 처리되었음을 표시
        attackProcessed = true;
    }

    private void ApplyDamage()
    {
        // 공격 범위 내의 모든 콜라이더를 가져옴
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.bounds.size.x / 2);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = hitCollider.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.TakeDamage(1f); // 데미지 1 적용
                    Debug.Log("적에게 공격 성공");
                }

                RangedMonster rangedMonster = hitCollider.GetComponent<RangedMonster>();
                if (rangedMonster != null)
                {
                    rangedMonster.TakeDamage(1f); // 데미지 1 적용
                    Debug.Log("원거리 몬스터에게 공격 성공");
                }
            }
            else if (hitCollider.CompareTag("Boss"))
            {
                BossHP boss = hitCollider.GetComponent<BossHP>();
                if (boss != null)
                {
                    boss.TakeDamage(1f); // 데미지 1 적용
                    Debug.Log("보스에게 공격 성공");
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
