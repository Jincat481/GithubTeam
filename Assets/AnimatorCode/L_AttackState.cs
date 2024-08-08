using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class L_AttackState : StateMachineBehaviour
{
    Enemy enemy;
    GameObject weapon;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    enemy = animator.GetComponent<Enemy>();
    enemy.L_atkDelay=enemy.L_atkCooltime;
    for(int i = 0; i < 3; i++){
        weapon = GameObject.FindWithTag("Weapon"+i);
        if(weapon.GetComponent<Weapon>().Projectiles_Delay > 0){
            continue;
        }
        else{
            Shoot();
            break;
        }
        
    }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void Shoot()
    {
        weapon.GetComponent<Weapon>().isL_Attack = true;
        weapon.GetComponent<Weapon>().direction=true;
        weapon.GetComponent<Weapon>().CCl.enabled=true;
        // 객체를 복제하여 새로운 투사체 객체 생성
        // Instantiate(enemy.weapon, enemy.ShootTransform.position, Quaternion.identity); // Quaternion.identity는 회전값을 안 줌
    }
}
