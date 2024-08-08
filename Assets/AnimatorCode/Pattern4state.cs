using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Pattern4state : StateMachineBehaviour
{
    public GameObject projectilePrefab; // 투사체 프리팹
    public GameObject returningprojectilePrefab; // 돌아가는 투사체 프리팹
    public float projectileSpeed = 10f; // 투사체 속도
    public float lifetime= 1.5f;
    private Vector2[] directions = { // 8방향을 나타내는 벡터 배열
        new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1),
        new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)
    };
    public int[] PlayerHitCheck= new int[8]; // 플레이어가 맞았는지 체크하는 1차원 배열
    private int count = 0;
    public enum state{
        FiringProjectiles,
        WaitforObject,
        ProjectileDisappears
    }
    public state StateCheck;
    GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i < PlayerHitCheck.Length; i++){
            PlayerHitCheck[i] = 0;
        }
        enemy = GameObject.FindWithTag("Enemy");

        StateCheck = state.FiringProjectiles;
        if(StateCheck == state.FiringProjectiles){
            foreach(Vector2 direction in directions){
                FireProjectile(direction); // 투사체를 발사
            }
            StateCheck = state.WaitforObject;
        }

        if(StateCheck == state.WaitforObject){
            CoroutineHelper.Instance.StartCoroutine(WaitforObject());
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (StateCheck == state.ProjectileDisappears)
        {
            for (int i = 0; i < PlayerHitCheck.Length; i++)
            {
                if (PlayerHitCheck[i] == 1)
                {
                    count++;
                    break;
                }
            }

            if (count > 0)
            {
                animator.SetTrigger("PlayerHit");
            }
            else
            {
                animator.SetTrigger("FailPlayerHit");
            }
            count = 0;
            StateCheck = state.FiringProjectiles; // 상태 초기화
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.GetComponent<Enemy>().pattern4Delay = enemy.GetComponent<Enemy>().pattern4Cooltime;
    }

    void FireProjectile(Vector2 direction){
        // 투사체 생성
        GameObject projectile =  Instantiate(projectilePrefab, enemy.transform.position, Quaternion.identity);

        // 투사체의 회전값 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 투사체에 속도 부여
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

        // Pattern4Prefab 스크립트 추가하고 되돌아가는 프리팹을 설정
        Pattern4Prefab projectileScript = projectile.AddComponent<Pattern4Prefab>();
        projectileScript.projectileSpeed = projectileSpeed;
        projectileScript.returningProjectilePrefab = returningprojectilePrefab;
        projectileScript.lifetime = lifetime;
    }

    IEnumerator WaitforObject()
    {
        Debug.Log("WaitforObject 코루틴 시작");
        yield return new WaitUntil(() => StateCheck == state.ProjectileDisappears);
        Debug.Log("WaitforObject 코루틴 끝");
    }
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
