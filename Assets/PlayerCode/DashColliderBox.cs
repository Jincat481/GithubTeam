using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // 콜라이더를 저장할 변수 이름 변경

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // 콜라이더 컴포넌트 가져오기
        dashCollider.enabled = false; // 시작할 때 콜라이더 비활성화
    }

    void Update()
    {
        // 데쉬 상태일 때만 콜라이더 활성화
        dashCollider.enabled = controller.isDashing; 
    }

    // 충돌 처리 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (controller.isDashing) // 컨트롤러의 isDashing 상태 확인
        {
            ApplyDashDamage(collision.collider);
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

    // 트리거 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other);
            controller.currentdashcooldown = 0; // 대쉬 쿨타임 초기화
        }
        else if (other.CompareTag("Boss")) 
        {
            HandleBossCollision(other);
        }
    }

    private void HandleEnemyCollision(Collider2D other)
    {
        EnemyMove enemyMove = other.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(controller.damage); 
            Debug.Log("공격 성공; 방향: " + controller.attackDirection); 

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }

        RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(controller.damage); 
            Debug.Log("공격 성공; 방향: " + controller.attackDirection); 

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }
    }

    private void HandleBossCollision(Collider2D other)
    {
        BossHP boss = other.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(controller.damage);
            Debug.Log("보스 공격 성공; 방향: " + controller.attackDirection); 

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }
    }

    private void ReduceDashCooldown()
    {
        if (!controller.cooldownReduced)
        {
            controller.currentdashcooldown -= 1f;
            if (controller.currentdashcooldown < 0)
            {
                controller.currentdashcooldown = 0;
            }
            controller.cooldownReduced = true; // 쿨다운 감소가 한 번만 일어나도록 설정
        }
    }
}
