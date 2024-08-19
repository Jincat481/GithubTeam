using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float health;
    // 포션 오브젝트
    [SerializeField]
    private GameObject Potion;

    private GameObject Player;
    private Animator anim;
    public float moveSpeed;
    public float spawnDelay;

    Rigidbody2D rigid;
    NavMeshAgent agent;
    Collider2D cl;
    public CapsuleCollider2D ChildCollider;

    // 사운드 매니저
    private GameObject soundManger;
    private MsoundManger soundMangerScript;

    bool ishurt = false;
    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
        StartCoroutine(StartDelay());
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsIdle", true);

        soundManger = GameObject.FindWithTag("MsoundManger");
        soundMangerScript = soundManger.GetComponent<MsoundManger>();
        cl = GetComponent<Collider2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        float directiontoPlayer = Player.transform.position.x - transform.position.x;

        if (health > 0f)
        {
            if (directiontoPlayer < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // 왼쪽 바라보게 설정
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0); // 오른쪽 방향 바라보게 설정
            }
        }
    }

    private void FixedUpdate()
    {
        // 플레이어 위치 체크해서 방향 바꾸기
        if (anim.GetBool("IsIdle"))
        {
            rigid.velocity = Vector2.zero;
        }
        else if (anim.GetBool("IsWalk") && health > 0)
        {
            agent.SetDestination(Player.transform.position);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        anim.SetBool("IsWalk", false);
        rigid.velocity = Vector3.zero;
        if (health > 0f)
        {
            agent.ResetPath();
            soundManger.GetComponent<AudioSource>().PlayOneShot(soundMangerScript.EnemyHurtAudio, soundMangerScript.EnemyHurtVolumeScale);
            anim.SetTrigger("IsHurt");
        }
        else
        {
            //int sel = Random.Range(0, dieAudioClip.Length);
            //AudioClip selectedClip = dieAudioClip[sel];
            //audioSource.Stop();
            //audioSource.PlayOneShot(selectedClip, playOneShot.dieVolumeScale);

            cl.enabled = false;
            agent.enabled = false;
            ChildCollider.enabled = false;
            anim.SetTrigger("IsDie");
        }
    }

    // 애니메이션 관련된 코드들
    public void IsDead()
    {
        Destroy(gameObject);
        // HP포션 생성
        float RandomNum = Random.Range(0, 101);
        if (RandomNum <= 5)
        {
            Instantiate(Potion, transform.position, Quaternion.identity);
        }
        Debug.Log("일반몹 사망");
    }

    public void isHurt()
    {
        ishurt = true;
    }

    public void HurtEnd()
    {
        if (!anim.GetBool("IsIdle"))
        {
            ishurt = false;
            anim.SetBool("IsWalk", true);
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnDelay = 0;
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", true);
    }
}

