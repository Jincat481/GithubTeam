using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCollider : MonoBehaviour
{
    Skill1Projectile skill1Projectile;

    void Start()
    {
        skill1Projectile = GetComponent<Skill1Projectile>();
    }

    public void OnPlayerCollision()
    {
        skill1Projectile.Player.GetComponent<PlayerController>().TakeDamage(skill1Projectile.Damage);
        Destroy(gameObject);
    }

    public void OnWallsCollision()
    {
        Destroy(gameObject);
    }
}
