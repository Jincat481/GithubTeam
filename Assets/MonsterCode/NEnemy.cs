using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEnemy : MonoBehaviour
{
    public float speed;                      // 몬스터 이동 속도
    public Rigidbody2D target;               

    bool isLive = true;                      // 몬스터 생존 상태

    Rigidbody2D rigid;                       
    SpriteRenderer spriter;                  

    // 플레이어에게 줄 데미지
    public int damageAmount = 10;            // 몬스터가 플레이어에게 주는 데미지

    // 몬스터의 체력
    public int health = 30;                  // 몬스터의 초기 체력

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

        // target이 null인지 확인하고 할당
        if (target == null)
        {
            Debug.LogError("타겟 못찾음.");
        }
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    // 충돌 감지 메서드
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지를 주는 로직
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("몬스터가 플레이어에게 데미지를 줌: " + damageAmount);
            }
        }
    }

    // 몬스터가 데미지를 받는 메소드
    public void TakeDamage(int damage)
    {
        if (!isLive) return;

        health -= damage;
        Debug.Log($"몬스터가 {damage}의 데미지를 받음. 남은 체력: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    // 몬스터가 사망하는 메소드
    private void Die()
    {
        isLive = false;
        spriter.enabled = false; 
        Debug.Log("몬스터가 사망했습니다.");
        Destroy(gameObject); // 몬스터 오브젝트 삭제
    }
}
