using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject attackEffect; // 공격 이펙트
    public GameObject dashEffect;   // 대쉬 이펙트
    public GameObject damageEffect; // 피해 이펙트
    public GameObject deathEffect;  // 사망 이펙트

    private void Start()
    {
        // 초기 설정이 필요한 경우
    }

    // 공격 시 호출되는 메서드
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
        }
    }

    // 대쉬 시 호출되는 메서드
    public void PlayDashEffect()
    {
        if (dashEffect != null)
        {
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }
    }

    // 피해를 입었을 때 호출되는 메서드
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
    }

    // 사망 시 호출되는 메서드
    public void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
