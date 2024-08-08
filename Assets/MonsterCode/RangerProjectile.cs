using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // �÷��̾�� �浹 �� ������ �ֱ�
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject); // �������� �ְ� ���� ����ü�� �ı��մϴ�.
            return;
        }

        // ���� �浹 �� ����ü �ı�
        if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
