using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float speed;
    public Vector2 home;
    public float atkCooltime = 2f;
    public float atkRange = 2f;
    public float atkDelay;
    public float L_atkCooltime = 5f;
    public float L_atkMINRange = 3f;
    public float L_atkMAXRange = 5f;
    public float L_atkDelay;
    public float pattern3Delay = 0f;
    public float pattern4Cooltime = 10f;
    public float pattern4Delay;
    public Transform boxpos;
    public Vector2 boxSize;
    public float maxhealth = 100f;
    public float Enemyhealth = 100f;
    public float pattern2_health = 95f;
    private GameObject weapon;
    private Pattern3 pattern3;
    [SerializeField]
    public Transform ShootTransform;
    public float distanceToPlayer;
    Transform objectTransform;
    Quaternion rightRotation = Quaternion.Euler(0, 180, 0); // Y 축을 기준으로 180도 회전
    Quaternion leftRotation = Quaternion.Euler(0, 0, 0); // Y 축을 기준으로 0도로 세팅
    // 패턴 4 거리
    public float pattern4MinRange;
    public float pattern4MaxRange;

    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3
    }
    public Phase phase;
    private bool phase2changed = false;
    private bool phase3changed = false;
    void Start()
    {
        animator = GetComponent<Animator>(); // GetComponent를 사용하여 에니메이터에 접근
        player = GameObject.FindGameObjectWithTag("Player").transform;
        home = transform.position;
        objectTransform = gameObject.transform;
        pattern4Delay = pattern4Cooltime;
        phase = Phase.Phase1;
    }
    private void Update()
    {
        if (Enemyhealth <= 65 && !phase2changed)
        {
            phase = Phase.Phase2;
            phase2changed = true;
        }
        else if (Enemyhealth <= 10 && !phase3changed)
        {
            phase = Phase.Phase3;
            phase3changed = true;
        }
        distanceToPlayer = Vector2.Distance(player.position, transform.position); // 플레이어 거리를 재는 변수
        if (Enemyhealth <= 0f)
        {
            StartCoroutine(DisableInvincibilityAfterDelay(3.0f));
            Destroy(gameObject);
        }
        if (phase != Phase.Phase3)
        {
            // 딜레이 감소
            L_atkDelay -= L_atkDelay > 0 ? Time.deltaTime : 0;
            atkDelay -= atkDelay > 0 ? Time.deltaTime : 0;
            pattern3Delay -= pattern3Delay > 0 ? Time.deltaTime : 0;
            pattern4Delay -= (pattern4Delay > 0 && Enemyhealth <= 80) ? Time.deltaTime : 0;
            // 패턴3 로직 처리
            if (pattern3Delay <= 0)
            {
                int count = 0;
                int maxCount = (phase == Phase.Phase1) ? 1 : (phase == Phase.Phase2) ? 2 : 0;

                for (int i = 0; i < 3; i++)
                {
                    weapon = GameObject.FindWithTag("Weapon" + i);
                    if (weapon != null && weapon.GetComponent<Weapon>().Projectiles_Delay <= 0)
                    {
                        if (count < maxCount)
                        {
                            pattern3 = weapon.GetComponent<Pattern3>();
                            pattern3.SetStateRandomPosition();
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Debug.Log("패턴 3 카운팅 " + count);
                pattern3Delay = Random.Range(30, 40); // 30~40 사이의 랜덤한 딜레이 값 설정
            }
        }

    }
    // 플레이어의 위치와 몬스터 위치를 매개변수로 전달받아 바라보는 걸 바꿈(메소드)
    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            if (phase == Phase.Phase1)
            { // 1페이즈 애니메이션 세팅
                animator.SetFloat("Direction", -1);
                objectTransform.rotation = leftRotation;
            }
            else if (phase == Phase.Phase2)
            {  // 2페이즈 애니메이션 세팅
                animator.SetFloat("Direction", -2);
                objectTransform.rotation = leftRotation;
            }
            else
            { // 3페이즈
                animator.SetFloat("Direction", -3);
                objectTransform.rotation = leftRotation;
            }
        }
        else
        {
            if (phase == Phase.Phase1)
            {
                animator.SetFloat("Direction", -1);
                objectTransform.rotation = rightRotation;
            }
            else if (phase == Phase.Phase2)
            {
                animator.SetFloat("Direction", -2);
                objectTransform.rotation = rightRotation;
            }
            else
            {
                animator.SetFloat("Direction", -3);
                objectTransform.rotation = rightRotation;
            }
        }
    }
    public void Attack()
    {
        if (animator.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("damage");
            }
        }
    }
    public void TakeDamage(float damage)
    {
        // 데미지를 백분율로 변환
        float damagePercentage = (damage / maxhealth) * 100f;
        Enemyhealth -= damagePercentage;

        Debug.Log($"보스 체력: {Enemyhealth}%");

        // 보스가 무적 상태가 되도록 설정
        animator.SetBool("isInvincible", true);
    }
    IEnumerator DisableInvincibilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isInvincible", false);
        Debug.Log("보스는 더 이상 무적 상태가 아닙니다.");
    }

    public void waitAnimation()
    { // 애니메이션 관련 코드
        animator.SetTrigger("Pattern4Start");
    }


}
