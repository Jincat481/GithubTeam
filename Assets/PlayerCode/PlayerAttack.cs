using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // 애니메이터 컴포넌트
    [SerializeField] private float meleeSpeed; // 공격 속도
    [SerializeField] private float damage = 1f; // 공격력
    [SerializeField] private Collider2D attackCollider; // 공격 범위 담당 콜라이더

    private float timeUntilMelee; // 공격 쿨타임
    private Vector2 attackDirection; // 공격 방향
    private bool attackProcessed; // 공격이 처리되었는지 여부를 나타내는 플래그

    private void Update()
    {
        // 쿨타임이 다 되었다면 공격 가능
        if (timeUntilMelee <= 0f)
        {
            // 마우스 좌클릭 시 공격
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 위치에 따라 공격 방향 설정
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                attackDirection = (mousePosition - (Vector2)transform.position).normalized;

                anim.SetTrigger("Attack"); // 애니메이션 트리거 호출
                timeUntilMelee = meleeSpeed; // 쿨타임 초기화
                attackProcessed = false; // 공격이 처리되지 않았음을 표시
            }
        }
        else
        {
            timeUntilMelee -= Time.deltaTime; // 쿨타임 감소
        }
    }

    // 애니메이션 이벤트에서 호출할 메서드
    private void ApplyDamage()
    {
        if (attackProcessed)
        {
            return; // 공격이 이미 처리된 경우 더 이상 진행하지 않음
        }

        // 공격 범위 내의 모든 콜라이더를 가져옴
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0f);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = hitCollider.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.TakeDamage(damage);
                    Debug.Log("공격 성공; 방향: " + attackDirection);
                }

                RangedMonster rangerMonster = hitCollider.GetComponent<RangedMonster>();
                if (rangerMonster != null)
                {
                    rangerMonster.TakeDamage(damage);
                    Debug.Log("공격 성공; 방향: " + attackDirection);
                }
            }
            else if (hitCollider.CompareTag("Boss"))
            {
                BossHP boss = hitCollider.GetComponent<BossHP>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                    Debug.Log("공격 성공; 방향: " + attackDirection);
                }
            }
        }

        attackProcessed = true; // 공격이 처리되었음을 표시
    }

    private void OnDrawGizmos()
    {
        if (attackCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);
        }
    }
}
