using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject attackEffect; // ���� ����Ʈ
    public GameObject leftDashEffect;   // ���� �뽬 ����Ʈ
    public GameObject rightDashEffect;  // ������ �뽬 ����Ʈ
    public GameObject damageEffect; // ���� ����Ʈ
    public GameObject deathEffect;  // ��� ����Ʈ

    private void Start()
    {

    }

    // ���� ����Ʈ
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ���� �뽬 ����Ʈ
    public void PlayLeftDashEffect()
    {
        if (leftDashEffect != null)
        {
            GameObject effect = Instantiate(leftDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ������ ���� ����Ʈ
    public void PlayRightDashEffect()
    {
        if (rightDashEffect != null)
        {
            GameObject effect = Instantiate(rightDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // �ǰݽ� ����Ʈ
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ��� ����Ʈ
    public void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    
    private void DestroyEffect(GameObject effect, float delay)
    {
        Destroy(effect, delay);
    }
}
