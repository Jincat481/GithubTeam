using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 플레이어와 충돌 시 데미지 주기
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject); // 데미지를 주고 나서 투사체를 파괴합니다.
            return;
        }

        // 벽과 충돌 시 투사체 파괴
        if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
