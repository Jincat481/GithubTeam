using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim; // 애니메이터 컴포넌트
    [SerializeField] private float meleeSpeed = 0.5f; // 공격 속도
    [SerializeField] private float damage = 1f; // 공격 데미지
    [SerializeField] private Collider2D attackCollider; // 공격 콜라이더 (Is Trigger로 설정)
    [SerializeField] private PlayerController playerController; // PlayerController 참조

    // 이펙트 및 사운드 관련 변수
    [SerializeField] private PlayerEffects playerEffects; // 플레이어 이펙트 스크립트
    [SerializeField] private SoundManager soundManager; // 사운드 매니저

    private float timeUntilMelee; // 공격 쿨타임
    private bool isAttackColliderActive = false; // 공격 콜라이더 활성화 상태

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
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
            
            anim.SetTrigger("Attack");

            
            StartCoroutine(AttackCoroutine());

            
            timeUntilMelee = meleeSpeed;

            
            if (playerEffects != null)
            {
                playerEffects.PlayAttackEffect();
            }

            
            if (soundManager != null)
            {
                soundManager.Play("Attack");
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        
        float attackAnimationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        
        isAttackColliderActive = true;
        attackCollider.enabled = true;

        
        yield return new WaitForSeconds(attackAnimationLength);

        
        isAttackColliderActive = false;
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (isAttackColliderActive)
        {
            bool damageApplied = false; 

            if (other.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = other.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.TakeDamage(damage); 
                    Debug.Log("적에게 공격 성공");
                    damageApplied = true;
                }

                RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
                if (rangedMonster != null)
                {
                    rangedMonster.TakeDamage(damage);
                    Debug.Log("원거리 몬스터에게 공격 성공");
                    damageApplied = true;
                }
            }
            else if (other.CompareTag("Boss"))
            {
                BossHP boss = other.GetComponent<BossHP>();
                if (boss != null)
                {
                    boss.TakeDamage(damage); 
                    Debug.Log("보스에게 공격 성공");
                    damageApplied = true;
                }
            }

            
            if (damageApplied)
            {
                ReduceDashCooldown();
            }
        }
    }

    private void ReduceDashCooldown()
    {
       
        if (playerController != null && playerController.currentdashcooldown > 0)
        {
            playerController.currentdashcooldown -= 1f;
            Debug.Log("대쉬 쿨타임 1초 감소");
        }
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
