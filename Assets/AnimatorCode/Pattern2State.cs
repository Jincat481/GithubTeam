using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class Pattern2State : StateMachineBehaviour
{
  Enemy enemy;
  Transform enemyTransform;
  public float minX = 10.5f;
  public float maxX= 21.7f;
  private float randomX;
  public float TeleportDealy = 2.5f;
  private bool isTeleporting = false;
  // 순간이동 후 레이저 발사 패턴
  public Transform Player;
  public GameObject warningLinePrefab; // 경고 선 프리팹
  public GameObject LaserPrefab0;
  public GameObject LaserPrefab1;
  public GameObject LaserPrefab2;
  Vector3 directionToPlayer;
  public float warningTime = 3f;
  private bool isFire = false;
  GameObject laser1, laser2, laser3;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy = animator.GetComponent<Enemy>();
       enemyTransform = animator.GetComponent<Transform>();
       Player = GameObject.FindWithTag("Player").transform;
       // 랜덤한 값을 생성
       randomX = Random.Range(minX,maxX);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      TeleportDealy -= Time.deltaTime;

      // Debug.Log("텔레포트까지 남은시간 : " +TeleportDealy);

      if(TeleportDealy <= 0 && !isTeleporting){
        enemyTransform.position = new Vector2(randomX, 5.56f); // 쿼터뷰에 맞게 y값 수정
        isTeleporting = true;
      }
      // 텔레포트가 끝났을 때
      if(TeleportDealy <= 0f && isTeleporting && !isFire){
        CoroutineHelper.Instance.StartCoroutine(ShowWarningAndfire());
      }
    }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      isTeleporting = false;
      isFire=false;
      
      
    }
    IEnumerator ShowWarningAndfire()
    {
      isFire = true;

      // 플레이어 방향 계산
      Vector2 directionToPlayer = (Player.position - enemyTransform.position).normalized;
      // 레이저 생성 위치 계산
      Vector2 laserStartPosition = (enemyTransform.position + Player.position) / 2;
      // 3개의 경고 선 표시
      GameObject warningLine1 = Instantiate(warningLinePrefab, enemyTransform.position, Quaternion.identity);
      GameObject warningLine2 = Instantiate(warningLinePrefab, enemyTransform.position, Quaternion.identity);
      GameObject warningLine3 = Instantiate(warningLinePrefab, enemyTransform.position, Quaternion.identity);
      // 각 경고 선의 방향 설정
      float angle1 = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 15;
      float angle2 = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
      float angle3 = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 15;

      warningLine1.transform.rotation = Quaternion.Euler(0, 0, angle1);
      warningLine2.transform.rotation = Quaternion.Euler(0, 0, angle2);
      warningLine3.transform.rotation = Quaternion.Euler(0, 0, angle3);

      /* Vector3 direction1 = Quaternion.Euler(0f, -15f, 0f) * directionToPlayer; // x값, y값, z값 vector3는 3D환경에서 사용
      Vector3 direction2 = directionToPlayer;
      Vector3 direction3 = Quaternion.Euler(0f, 15f, 0f) * directionToPlayer;
      // 경고 선의 위치 및 방향 설정
      warningLine1.transform.rotation = Quaternion.LookRotation(direction1);
      warningLine2.transform.rotation = Quaternion.LookRotation(direction2);
      warningLine3.transform.rotation = Quaternion.LookRotation(direction3);
      */

      // 길이 조절
      //warningLine1.transform.localScale = new Vector3(1f, 1f, warningTime * projectileSpeed); // 길이 조절
      //warningLine2.transform.localScale = new Vector3(1f, 1f, warningTime * projectileSpeed); // 길이 조절
      //warningLine3.transform.localScale = new Vector3(1f, 1f, warningTime * projectileSpeed); // 길이 조절

      yield return new WaitForSeconds(warningTime);
      // 경고 선 삭제
      Destroy(warningLine1);
      Destroy(warningLine2);
      Destroy(warningLine3);
      
      // 레이저 생성
      GameObject laser1 = Instantiate(LaserPrefab0, enemyTransform.position, Quaternion.identity);
      GameObject laser2 = Instantiate(LaserPrefab1, enemyTransform.position, Quaternion.identity);
      GameObject laser3 = Instantiate(LaserPrefab2, enemyTransform.position, Quaternion.identity);

      laser1.transform.rotation = Quaternion.Euler(0, 0, angle1);
      laser2.transform.rotation = Quaternion.Euler(0, 0, angle2);
      laser3.transform.rotation = Quaternion.Euler(0, 0, angle3);
      
      yield return new WaitForSeconds(warningTime);

      // Destroy(laser1);
      // Destroy(laser2);
      // Destroy(laser3);
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
