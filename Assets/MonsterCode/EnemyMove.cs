using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float health;
    // 포션 오브젝트
    [SerializeField]
    private GameObject Potion;
    private GameObject Player;
    [SerializeField]
    private bool Idle = true;
    public float moveSpeed;
    public float spawnDelay;
    Rigidbody2D rigid;
    NavMeshAgent agent;
    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
        StartCoroutine(StartDelay());
        rigid = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    private void FixedUpdate() 
    {
        if(Idle)
        {
            rigid.velocity = Vector2.zero;
        }
        else{
            agent.SetDestination(Player.transform.position);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Destroy(gameObject);
            // HP포션 생성
            float RandomNum = Random.Range(0, 101);
            if (RandomNum <= 5)
            {
                Instantiate(Potion, transform.position, Quaternion.identity);
            }
            Debug.Log("���� ���");
        }
    }

    IEnumerator StartDelay(){
        yield return new WaitForSeconds(spawnDelay);
        
        Idle = false;
    }
}

