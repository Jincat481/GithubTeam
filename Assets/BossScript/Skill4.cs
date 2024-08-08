using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Skill4 : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    
    public float animatorSpeed;
    BossSkill bossSkill;
    public float SkillretentionTime;
    Collider2D cl;
    new Renderer renderer;
    public Vector2 direction;
    public float Damage;
    GameObject Player;
    Renderer Skill4AreaRenderer;
    public MonsterSpawnerMK2 monsterSpawner;
    public void Start()
    {
        bossSkill = GetComponentInParent<BossSkill>();
        cl = GetComponent<PolygonCollider2D>();
        renderer = GetComponent<Renderer>();
        Player = GameObject.FindWithTag("Player");
        Skill4AreaRenderer = GameObject.FindWithTag("skill4Area").GetComponent<Renderer>();
        
    }
    public void Update()
    {

    }

    public void Skill4Start()
    {
        renderer.enabled = true;

        // 플레이어 위치 기준 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        // 애니메이션 속도 조절
        anim.SetFloat("AnimationSpeed", animatorSpeed);

        // 스킬 애니메이션 실행
        anim.SetTrigger("Skill4Start");

        // 애니메이션이 시작하면서 몬스터 스폰
        monsterSpawner.spawn = true;
    }

    public void EnableSkill4()
    {
        // 스킬 애니메이션이 끝난 후 실행
        StartCoroutine(EnableSkill4_1());
    }

    IEnumerator EnableSkill4_1()
    {
        cl.enabled = true;
        Skill4AreaRenderer.enabled = true;
        yield return new WaitForSeconds(SkillretentionTime);
        cl.enabled = false;
        Skill4AreaRenderer.enabled = false;
        renderer.enabled = false;
        bossSkill.skill4Data.Skill4End = true;
    }

    private void OnTriggerEnter2D(Collider2D o) {
        if(o.gameObject.tag == "Player"){
            Player.GetComponent<PlayerController>().TakeDamage(Damage);
        }
    }
}
