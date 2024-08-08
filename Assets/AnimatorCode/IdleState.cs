using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    GameObject Player;
    BoxCollider2D bossbackground; // 보스 백그라운드 영역을 나타내는 콜라이더

    // 아이들 상태에 진입할 때
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found. Ensure the player has the 'Player' tag.");
            return;
        }

        enemy = animator.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Enemy component not found on the animator's game object.");
            return;
        }

        enemyTransform = animator.GetComponent<Transform>();
        if (enemyTransform == null)
        {
            Debug.LogError("Transform component not found on the animator's game object.");
            return;
        }

        bossbackground = GameObject.FindWithTag("BossBackGround").GetComponent<BoxCollider2D>();
        if (bossbackground == null)
        {
            Debug.LogError("BossBackGround collider not found. Ensure there is a GameObject with the 'BossBackGround' tag and a BoxCollider2D component.");
            return;
        }
    }

    // 아이들 상태가 진행중 일 때
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Player.transform.position, enemy.transform.position) <= 20f)
        {
            animator.SetBool("IsFollow", true);
            Debug.Log("플레이어가 현재 보스방에 있습니다.");
        }
        else
        {
            animator.SetBool("IsFollow", false);
        }
    }

    // 상태를 나갈 때
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
