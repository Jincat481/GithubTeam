using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill1Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    Transform PlayerTransform;
    public float Speed;
    public float Damage;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerTransform = Player.transform;
        direction = PlayerTransform.position - transform.position;

        // 플레이어에게 날아가는 속도 수정
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * Speed;

        // 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 각도 설정
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
