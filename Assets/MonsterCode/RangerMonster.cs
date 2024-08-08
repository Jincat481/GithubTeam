using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    public float health = 100f; 
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireDelay = 2f;
    public float projectileSpeed = 5f;
    public float detectionRange = 10f;
    [ReadOnly]
    public float CurrentFireDelay = 0f;
    private Transform target;

    void Start()
    {
        // Ÿ���� �÷��̾�� ���� (�÷��̾� �±װ� "Player"�� ���)
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null)
            return;

        if (CurrentFireDelay > 0)
        {
            CurrentFireDelay -= Time.deltaTime;
        }
        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        if (distanceToTarget <= detectionRange && CurrentFireDelay <= 0)
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - firePoint.position).normalized;
        rb.velocity = direction * projectileSpeed;

        CurrentFireDelay = fireDelay;
    }


    public void TakeDamage(float damage)
    {
        health -= damage; 

        if (health <= 0f)
        {
            Die(); 
        }
    }

    private void Die()
    {
        Debug.Log("���Ͱ� �׾����ϴ�."); 
        gameObject.SetActive(false); 
    }
}

