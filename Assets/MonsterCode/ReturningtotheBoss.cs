using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReturningtotheBoss : MonoBehaviour
{
    GameObject enemy;
    Collider2D cl;
    Transform playertransform;
    bool playerstrun;
    GameObject player;
    Pattern4state pattern4state;
    private bool playerhitcheck = false;
    public float Distancetoenemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        cl = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        playertransform = player.transform;
        Animator enemyAnimator = enemy.GetComponent<Animator>();
        
        // StateMachineBehaviour에서 Pattern4state 컴포넌트를 가져오는 방법
        pattern4state = enemyAnimator.GetBehaviour<Pattern4state>();
        
        // playerStun = player.GetComponent<PlayerController>().playerStun;
    }

    // Update is called once per frame
    void Update()
    {
        if(cl.bounds.Contains(playertransform.position)){ // 플레이어가 오브젝트의 영역안에 있을 때
            // playerStun = true;
            playertransform.position = transform.position;
        }
        Distancetoenemy = Vector2.Distance(transform.position, enemy.transform.position);
        if(Distancetoenemy < 0.3f){
            // playerStun = false;
            if(cl.bounds.Contains(playertransform.position)){ // 되돌아오는 투사체 영역 안에 플레이어가 있다면
            Debug.Log("Player is within bounds");
                if(!playerhitcheck){
                    for(int i = 0; i < pattern4state.PlayerHitCheck.Length; i++){ // pattern4에 든 플레이어 히트체크 변수의 크기만큼 반복
                        if(pattern4state.PlayerHitCheck[i] == 0){
                            pattern4state.PlayerHitCheck[i] = 1;
                            Debug.Log("PlayerHitCheck[" + i + "] set to 1");
                            break;
                        }
                    }
                    playerhitcheck = true; // 이 변수가 true로 설정되도록 함
                }
            }
            pattern4state.StateCheck = Pattern4state.state.ProjectileDisappears;
            Debug.Log("StateCheck : "+pattern4state.StateCheck);
            Destroy(gameObject);
        }
    }
}
