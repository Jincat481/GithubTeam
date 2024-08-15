using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5 : MonoBehaviour
{
    new Renderer renderer;
    Renderer Skill5AreaRenderer;
    GameObject Skill5Area;
    GameObject Player;
    Animator anim;
    public float animationSpeed;
    public float SkillretentionTime;
    public float Damage;
    Collider2D cl;
    BossSkill bossSkill;
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Skill5Area = transform.GetChild(1).gameObject;
        Skill5AreaRenderer = Skill5Area.GetComponent<Renderer>();
        Player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        renderer.enabled = false;
        bossSkill = GetComponentInParent<BossSkill>();
        cl = GetComponent<Collider2D>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill5Start()
    {
        // 월드 좌표 기준으로 위치 수정
        Vector3 targetWorldPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        transform.localPosition = transform.parent.InverseTransformPoint(targetWorldPosition);

        // 스프라이트 렌더 활성화
        renderer.enabled = true;

        // 애니메이션 속도 조절
        anim.SetFloat("AnimationSpeed", animationSpeed);

        // 스킬 애니메이션 실행
        anim.SetTrigger("Skill5Start");
    }

    public void EnableSkill5()
    {
        // 스킬 애니메이션이 끝난 후 실행
        StartCoroutine(EnableSkill5_1());
    }

    IEnumerator EnableSkill5_1()
    {
        cl.enabled = true;
        Skill5AreaRenderer.enabled = true;
        particle.Play();
        yield return new WaitForSeconds(SkillretentionTime);
        particle.Stop();
        particle.Clear();
        cl.enabled = false;
        Skill5AreaRenderer.enabled = false;
        renderer.enabled = false;
        bossSkill.skill5Data.Skill5End = true;
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        if(o.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerController>().TakeDamage(Damage);
        }
    }
}
