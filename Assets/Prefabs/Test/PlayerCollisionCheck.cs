using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnParticleCollision(GameObject o) 
    {
        if (o.gameObject.CompareTag("Player"))
        {
            o.GetComponent<PlayerController>().TakeDamage(Damage);
            ObjectPoolManger.ReturnObjectToPool(gameObject, ObjectPoolManger.PoolType.GameObject);
        }
    }
}
