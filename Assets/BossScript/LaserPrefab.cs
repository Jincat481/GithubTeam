using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPrefab : MonoBehaviour
{
    Collider2D cl;
    GameObject player;
    public float rotationSpeed;
    public float Damage;
    public float ObjectDestroytime;
    public float initialAngle; // 초기 회전 값을 저장할 변수

    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider2D>();
        cl.enabled = true;
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        initialAngle += deltaTime * rotationSpeed; // 회전을 누적시킴

        // 초기 회전 값에 누적된 회전을 적용
        transform.rotation = Quaternion.Euler(0, 0, initialAngle);

        Destroy(gameObject, ObjectDestroytime);
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage(Damage);
        }
    }
}
