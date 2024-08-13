using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject attackEffect; // ���� ����Ʈ
    public GameObject dashEffect;   // �뽬 ����Ʈ
    public GameObject damageEffect; // ���� ����Ʈ
    public GameObject deathEffect;  // ��� ����Ʈ

    private void Start()
    {
        // �ʱ� ������ �ʿ��� ���
    }

    // ���� �� ȣ��Ǵ� �޼���
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
        }
    }

    // �뽬 �� ȣ��Ǵ� �޼���
    public void PlayDashEffect()
    {
        if (dashEffect != null)
        {
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }
    }

    // ���ظ� �Ծ��� �� ȣ��Ǵ� �޼���
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
    }

    // ��� �� ȣ��Ǵ� �޼���
    public void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
