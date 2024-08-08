using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    private float lastAttackTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy=animator.GetComponent<Enemy>();
       enemyTransform=animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(enemy.distanceToPlayer <= enemy.atkRange && enemy.atkDelay<=0){
            animator.SetTrigger("Attack");
        }
        if(enemy.distanceToPlayer > enemy.atkRange) {
            animator.SetBool("IsFollow",true);
            animator.SetBool("IsReady", false);
        }

        if (enemy.Enemyhealth <= enemy.pattern2_health){ // 순간이동 패턴
            animator.SetTrigger("Pattern2");
            enemy.pattern2_health -= 5f;
        }
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }
}