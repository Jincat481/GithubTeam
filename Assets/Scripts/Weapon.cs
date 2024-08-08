using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    public GameObject Weapon_Position;
    private Vector3 directionToPlayer;
    private Vector3 directionToEnemy;
    public float flytime;
    [SerializeField]
    private float moveSpeed = 7;
    public CircleCollider2D CCl;
    public bool isL_Attack = false;
    public bool direction = false;
    public bool Player_parrying_collision = false;
    public float Projectiles_Delay = 0f; // 원거리 공격 쿨타임


    void Start()
    {
        CCl = GetComponent<CircleCollider2D>();
        CCl.isTrigger = true;
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다. 'Player' 태그가 제대로 설정되었는지 확인하세요.");
        }
    }


    void Update()
    {
        // 투사체 무력화 시간이 0 이고 공격 중이 아닐 때
        if (Projectiles_Delay <= 0f && !isL_Attack)
        {
            transform.position = Weapon_Position.transform.position;
        }
        // 무력화 시간이 0보다 크면
        if (Projectiles_Delay >= 0f)
        {
            Projectiles_Delay -= Time.deltaTime;
        }
        // 공격 시작 된 경우
        if (isL_Attack && Projectiles_Delay <= 0f)
        {
            Player_parrying_collision = false;
            flytime += Time.deltaTime;

            if (flytime < 1.5f)
            {
                if (direction)
                {
                    directionToPlayer = (Player.transform.position - transform.position).normalized;
                    direction = false;
                }
                transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
            }
            else if (flytime > 1.5f)
            {
                // 보스 위치 기반으로 directionToEnemy 재계산
                directionToEnemy = (Weapon_Position.transform.position - transform.position).normalized;
                transform.position += directionToEnemy * moveSpeed * Time.deltaTime;
            }

            // Weapon_position에 도착한 경우 플레이어가 패링한 경우
            if ((Vector3.Distance(transform.position, Weapon_Position.transform.position) <= 0.3f && flytime >= 1.5f) || Player_parrying_collision)
            {
                CCl.enabled = false; // Collider 컴포넌트 비활성화
                isL_Attack = false;
                flytime = 0f;
                direction = true;
            }

            if (flytime >= 3f || Player_parrying_collision)
            {
                CCl.enabled = false; // Collider 컴포넌트 비활성화
                isL_Attack = false;
                flytime = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        var playerController = Player.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("Player 객체에 PlayerController 컴포넌트가 없습니다.");
            return;
        }

        
        if ( o.gameObject.tag == "Player")
        {
            playerController.TakeDamage(10);
            flytime = 1.5f; // 비행 시간을 1.5로 설정하여 바로 제자리로 돌아가게 만듬
        }
    }
}
