using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    GameObject Player;
    BoxCollider2D bossbackground; // 보스 백그라운드 영역을 나타내는 콜라이더
    GameObject weapon;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        bossbackground = GameObject.FindWithTag("BossBackGround").GetComponent<BoxCollider2D>();
        Player = GameObject.FindWithTag("Player");
        // weapon = GameObject.FindWithTag("Weapon"+i).GetComponent<Weapon>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 플레이어가 백그라운드 영역을 벗어났을 때 Idle 상태로 전환 
        if (Vector2.Distance(Player.transform.position, enemy.transform.position) > 20f)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsFollow", false);
            return; // 추가 작업을 중지하고 바로 반환
        }

        // 플레이어가 백그라운드 영역 안에 있을 때의 동작
        if (Vector2.Distance(Player.transform.position, enemy.transform.position) <= 20f)
        {
            if (enemy.phase != Enemy.Phase.Phase3) // 마지막 페이즈에서 레디상태로 진입과 움직임을 못하게 설정 
            {
                if (enemy.distanceToPlayer > enemy.atkRange) 
                {   // 플레이어 방향으로 이동
                    enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.speed);
                }
                else if (enemy.distanceToPlayer <= enemy.atkRange)
                {   // 준비 상태
                    animator.SetBool("IsReady", true);
                    animator.SetBool("IsFollow", false);
                }
                for (int i = 0; i < 3; i++)
                {
                    weapon = GameObject.FindWithTag("Weapon" + i);
                    if (weapon.GetComponent<Weapon>().Projectiles_Delay > 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (enemy.L_atkDelay <= 0 && enemy.distanceToPlayer > enemy.L_atkMINRange && enemy.distanceToPlayer <= enemy.L_atkMAXRange)
                        { // 투사체의 딜레이가 0이고 원거리 공격 딜레이가 0보다 작으면 원거리 공격 상태로
                            animator.SetTrigger("L_Attack");
                        }
                        break;
                    }

                }

                if (enemy.pattern4Delay <= 0 && enemy.distanceToPlayer > enemy.pattern4MinRange && enemy.distanceToPlayer <= enemy.pattern4MaxRange)
                {
                    animator.SetTrigger("Pattern4ReadyAnimation");
                }

                if (enemy.Enemyhealth <= enemy.pattern2_health)
                { // 순간이동 패턴
                    animator.SetTrigger("Pattern2");
                    enemy.pattern2_health -= 5f;
                }
            }
        }
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
