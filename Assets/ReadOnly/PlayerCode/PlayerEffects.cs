using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject attackEffect; // ∞¯∞› ¿Ã∆Â∆Æ
    public GameObject leftDashEffect;   // øﬁ¬  ¥ÎΩ¨ ¿Ã∆Â∆Æ
    public GameObject rightDashEffect;  // ø¿∏•¬  ¥ÎΩ¨ ¿Ã∆Â∆Æ
    public GameObject damageEffect; // «««ÿ ¿Ã∆Â∆Æ
    public GameObject deathEffect;  // ªÁ∏¡ ¿Ã∆Â∆Æ

    private void Start()
    {

    }

    // ∞¯∞› ¿Ã∆Â∆Æ
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // øﬁ¬  ¥ÎΩ¨ ¿Ã∆Â∆Æ
    public void PlayLeftDashEffect()
    {
        if (leftDashEffect != null)
        {
            GameObject effect = Instantiate(leftDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ø¿∏•¬  µ•Ω¨ ¿Ã∆Â∆Æ
    public void PlayRightDashEffect()
    {
        if (rightDashEffect != null)
        {
            GameObject effect = Instantiate(rightDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ««∞›Ω√ ¿Ã∆Â∆Æ
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ªÁ∏¡ ¿Ã∆Â∆Æ
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
