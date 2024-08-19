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

    // ���� �� ȣ��Ǵ� �޼���
    public void PlayAttackEffect()
    {
        if (attackEffect != null)
        {
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f);
        }
    }

    // ���� �뽬 �� ȣ��Ǵ� �޼���
    public void PlayLeftDashEffect()
    {
        if (leftDashEffect != null)
        {
            GameObject effect = Instantiate(leftDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // ������ �뽬 �� ȣ��Ǵ� �޼���
    public void PlayRightDashEffect()
    {
        if (rightDashEffect != null)
        {
            GameObject effect = Instantiate(rightDashEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // ���ظ� �Ծ��� �� ȣ��Ǵ� �޼���
    public void PlayDamageEffect()
    {
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // ��� �� ȣ��Ǵ� �޼���
    public void PlayDeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            DestroyEffect(effect, 2f); 
        }
    }

    // ����Ʈ�� ���� �ð� �Ŀ� �����ϴ� �޼���
    private void DestroyEffect(GameObject effect, float delay)
    {
        Destroy(effect, delay);
    }
}
