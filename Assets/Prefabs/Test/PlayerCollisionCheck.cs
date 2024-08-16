using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    private Skill1Projectile skill1Projectile;

    // Start is called before the first frame update
    void Start()
    {
        skill1Projectile = GetComponent<Skill1Projectile>();
        if (skill1Projectile == null)
        {
            Debug.LogError("skill1Projectile 컴포넌트를 찾을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnParticleCollision(GameObject o) 
    {
        if (o.gameObject.CompareTag("Player"))
        {
            skill1Projectile.Player.GetComponent<PlayerController>().TakeDamage(skill1Projectile.Damage);
            ObjectPoolManger.ReturnObjectToPool(gameObject);
        }
    }
}
