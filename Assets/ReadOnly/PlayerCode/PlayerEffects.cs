using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject attackEffect; // 공격 이펙트
    public GameObject leftDashEffect;   // 왼쪽 대쉬 이펙트
    public GameObject rightDashEffect;  // 오른쪽 대쉬 이펙트
    public GameObject damageEffect; // 피해 이펙트
    public GameObject deathEffect;  // 사망 이펙트

    private void Start()
    {

    }

    // 공격 시 호출되는 메서드
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // 왼쪽 대쉬 시 호출되는 메서드
    public void PlayLeftDashEffect()
    {
        if (leftDashEffect != null)
        {
            GameObject effect = Instantiate(leftDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // 오른쪽 대쉬 시 호출되는 메서드
    public void PlayRightDashEffect()
    {
        if (rightDashEffect != null)
        {
            GameObject effect = Instantiate(rightDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // 피해를 입었을 때 호출되는 메서드
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // 사망 시 호출되는 메서드
    public void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // 이펙트를 일정 시간 후에 삭제하는 메서드
    private void DestroyEffect(GameObject effect, float delay)
    {
        Destroy(effect, delay);
    }
}
