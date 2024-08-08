using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4Prefab : MonoBehaviour
{
    public GameObject returningProjectilePrefab; // 되돌아가는 투사체 프리팹
    public float projectileSpeed; // 투사체 속도
    public float lifetime; // 투사체의 수명 (초)
    private float spawnTime;
    GameObject enemy;
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        spawnTime = Time.time; // 투사체가 생성된 시간 기록
    }

    void Update()
    {
        // 투사체가 수명을 다하면 파괴
        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
            returningProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 기존 투사체 파괴
            Destroy(gameObject);
            returningProjectile();
            
        }
    }
    
    void returningProjectile(){
        // 되돌아가는 투사체 생성
        GameObject returningProjectile = Instantiate(returningProjectilePrefab, transform.position, Quaternion.identity);

        // 되돌아가는 투사체의 방향을 적의 위치로 설정
        Vector2 direction = (GameObject.FindWithTag("Enemy").transform.position - transform.position).normalized;

        // 투사체의 회전값 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        returningProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 되돌아가는 투사체에 속도 부여
        Rigidbody2D rb = returningProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }
}

internal class Patten4state
{
}