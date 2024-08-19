using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMonsterCollision : MonoBehaviour
{
    [SerializeField]private float CollisionDamage;
    NavMeshAgent agent;
    PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Player") && !playerScript.isDashing)
        {
            playerScript.TakeDamage(CollisionDamage);
            agent.ResetPath();
        }
    }
}
