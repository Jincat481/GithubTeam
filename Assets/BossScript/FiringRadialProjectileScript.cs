using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringRadialProjectileScript : MonoBehaviour
{
    GameObject Player;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D o) 
    {
        if(o.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerController>().TakeDamage(Damage);
        }

        if(o.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
