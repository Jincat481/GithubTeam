using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEnemy : MonoBehaviour
{
    public float speed;                      // ���� �̵� �ӵ�
    public Rigidbody2D target;               

    bool isLive = true;                      // ���� ���� ����

    Rigidbody2D rigid;                       
    SpriteRenderer spriter;                  

    // �÷��̾�� �� ������
    public int damageAmount = 10;            // ���Ͱ� �÷��̾�� �ִ� ������

    // ������ ü��
    public int health = 30;                  // ������ �ʱ� ü��

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

        // target�� null���� Ȯ���ϰ� �Ҵ�
        if (target == null)
        {
            Debug.LogError("Ÿ�� ��ã��.");
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

    // �浹 ���� �޼���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ���� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �������� �ִ� ����
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("���Ͱ� �÷��̾�� �������� ��: " + damageAmount);
            }
        }
    }

    // ���Ͱ� �������� �޴� �޼ҵ�
    public void TakeDamage(int damage)
    {
        if (!isLive) return;

        health -= damage;
        Debug.Log($"���Ͱ� {damage}�� �������� ����. ���� ü��: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    // ���Ͱ� ����ϴ� �޼ҵ�
    private void Die()
    {
        isLive = false;
        spriter.enabled = false; 
        Debug.Log("���Ͱ� ����߽��ϴ�.");
        Destroy(gameObject); // ���� ������Ʈ ����
    }
}
