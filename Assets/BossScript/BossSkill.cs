using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BossSkill : MonoBehaviour
{
    public float Delay_after_using_a_skill;
    public enum State
    {
        Idle = 0,
        Skill1 = 1,
        Skill2 = 2,
        Skill3 = 3,
        Skill4 = 4,
        Skill5 = 5
    }

    [System.Serializable]
    public class SkillData
    {
        public State state;
        public GameObject SkillPrefab;
        public float Cooldown;
        [ReadOnly]
        public float CurrentCooldown;

        public float Damage;
        public float ObjectSpeed;
    }

    [System.Serializable]
    public class Skill1Data : SkillData
    {
        public float Duration;
        public int ObjectCount; // int로 수정
    }

    [System.Serializable]
    public class Skill2Data : SkillData
    {
        public GameObject Skill2_WarningPrefab;
        public float addAngle;
        public int objectCount;
        public float warningTime;
        public float ObjectDestroytime;
    }

    [System.Serializable]
    public class Skill3Data : SkillData
    {
        public float angleToAdd;
        public int ObjectCount;
    }

    [System.Serializable]
    public class Skill4Data : SkillData // 스킬4에선 오브젝트 스피드는 애니메이션 스피드임
    {
        public float SkillretentionTime; // 스킬 유지 시간
        [ReadOnly]
        public bool Skill4End = false;
    }

    [System.Serializable]
    public class Skill5Data : SkillData
    {
        public float SkillretentionTime;
        [ReadOnly]
        public bool Skill5End = false;
    }

    public Skill1Data skill1Data;
    public Skill2Data skill2Data;
    public Skill3Data skill3Data;
    public Skill4Data skill4Data;
    public Skill5Data skill5Data;
    public State currentState;
    private Dictionary<State, SkillData> SkillDataDictionary;

    private GameObject Player;
    private Skill4 skill4Script;
    private Skill5 skill5Script;
    Vector2 directiontoplayer;
    public bool test;
    void Start()
    {
        currentState = State.Idle;
        InitializeSkillDataDictionary();
        Player = GameObject.FindWithTag("Player");
        skill4Script = GetComponentInChildren<Skill4>();
        skill5Script = GetComponentInChildren<Skill5>();
    }

    void Update()
    {
        foreach (var skillData in SkillDataDictionary.Values)
        {
            if (skillData.CurrentCooldown > 0)
            {
                skillData.CurrentCooldown -= Time.deltaTime;
            }
        }

        if (currentState == State.Idle)
        {
            if (!test)
            {
                int randomState = Random.Range(1, 6); // 1~ 6의 랜덤한 값을 뽑아냄
                currentState = (State)randomState;
            }
            if (test)
            {
                int enumlength = System.Enum.GetValues(typeof(State)).Length;
                for (int i = 0; i < enumlength; i++)
                {
                    if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i)))
                    {
                        currentState = (State)i;
                    }
                }
            }
            if (currentState != State.Idle)
            {
                SkillData currentSkillData = SkillDataDictionary[currentState];
                ExecuteSkill(currentSkillData);
            }
        }
    }

    void ExecuteSkill(SkillData currentSkillData)
    {
        if (currentSkillData.CurrentCooldown > 0)
        {
            Debug.Log("쿨타임이 아직 끝나지 않았습니다.");
            currentState = State.Idle;
            return;
        }

        // 스킬에 맞는 스킬 실행
        switch (currentSkillData.state)
        {
            case State.Skill1:
                Debug.Log("Skill1 실행");
                Skill1Data skill1 = (Skill1Data)currentSkillData;
                StartCoroutine(ExecuteSkill1WithDelay(skill1));
                break;
            case State.Skill2:
                Debug.Log("Skill2 실행");
                Skill2Data skill2 = (Skill2Data)currentSkillData;
                StartCoroutine(DelayafterSkill2execution(skill2));
                break;
            case State.Skill3:
                Debug.Log("Skill3 실행");
                Skill3Data skill3 = (Skill3Data)currentSkillData;

                FiringRadialProjectiles(skill3);

                StartCoroutine(SkillEndDelay(Delay_after_using_a_skill, skill3));
                break;
            case State.Skill4:
                Debug.Log("Skill4 실행");
                Skill4Data skill4 = (Skill4Data)currentSkillData;
                StartCoroutine(Skill4ExecuteAfterDelay(skill4));
                break;
            case State.Skill5:
                Debug.Log("Skill5 실행");
                Skill5Data skill5 = (Skill5Data)currentSkillData;
                StartCoroutine(Skill5ExecuteAfterDelay(skill5));
                break;
            default:
                Debug.LogWarning("알 수 없는 스킬 상태");
                currentState = State.Idle;
                break;
        }
        SkillDataDictionary[currentSkillData.state] = currentSkillData;
    }

    IEnumerator SkillEndDelay(float delay, SkillData skillData)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("스킬 후딜레이 " + delay + "초가 끝났습니다.");
        SetIdleAfterDelay(skillData);
    }

    void SetIdleAfterDelay(SkillData skillData)
    {
        skillData.CurrentCooldown = skillData.Cooldown;
        currentState = State.Idle;
    }

    IEnumerator ExecuteSkill1WithDelay(Skill1Data skill1)
    {
        for (int i = 0; i < skill1.ObjectCount; i++)
        {
            GameObject skill1Object = ObjectPoolManger.SpawnObject(skill1.SkillPrefab, transform.position, Quaternion.identity, ObjectPoolManger.PoolType.GameObject);
            Skill1Projectile skill1Script;
            if (skill1Object.GetComponent<Skill1Projectile>() == null)
            {
                skill1Script = skill1Object.AddComponent<Skill1Projectile>();
            }
            else
            {
                skill1Script = skill1Object.GetComponent<Skill1Projectile>();
            }
            skill1Script.Player = Player;
            skill1Script.Damage = skill1.Damage;
            skill1Script.Speed = skill1.ObjectSpeed;
            skill1Script.SetToPlayerPosition();
            yield return new WaitForSeconds(skill1.Duration);
        }
        StartCoroutine(SkillEndDelay(Delay_after_using_a_skill, skill1));

    }

    IEnumerator DelayafterSkill2execution(Skill2Data skill2)
    {
        if (skill2.objectCount <= 0)
        {
            Debug.Log("스킬2의 피사체 개수를 0보다 크게 설정하세요.");
            SetIdleAfterDelay(skill2);
            yield break; // objectCount가 0이거나 음수일 경우, 코루틴을 종료
        }
        // 현재 객체와 플레이어 객체 간의 방향 벡터 계산
        directiontoPlayer();

        // 각도 계산
        float angle = Mathf.Atan2(directiontoplayer.y, directiontoplayer.x) * Mathf.Rad2Deg;

        float[] initialAngle = new float[skill2.objectCount];
        // 초기 회전 값을 저장
        initialAngle[0] = angle;

        // 회전 값을 배열에 저장
        for (int i = 1; i < skill2.objectCount; i++)
        {
            initialAngle[i] = initialAngle[i - 1] + skill2.addAngle;
        }

        // 경고 선을 표시
        for (int i = 0; i < skill2.objectCount; i++)
        {
            GameObject skill2WarningObject = Instantiate(skill2.Skill2_WarningPrefab, transform.position, Quaternion.identity);

            skill2WarningObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialAngle[i]));

            WarningPrefab skill2WarningScript = skill2WarningObject.AddComponent<WarningPrefab>();
            skill2WarningScript.warningTime = skill2.warningTime;
        }
        yield return new WaitForSeconds(skill2.warningTime);

        // 경고 선 표시가 끝나면 피사체 발사
        for (int i = 0; i < skill2.objectCount; i++)
        {
            GameObject skill2Object = Instantiate(skill2.SkillPrefab, transform.position, Quaternion.identity);

            skill2Object.transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialAngle[i]));

            LaserPrefab skill2Script = skill2Object.AddComponent<LaserPrefab>();
            skill2Script.Damage = skill2.Damage;
            skill2Script.rotationSpeed = skill2.ObjectSpeed;
            skill2Script.initialAngle = initialAngle[i];
            skill2Script.ObjectDestroytime = skill2.ObjectDestroytime;
        }
        StartCoroutine(SkillEndDelay(skill2.ObjectDestroytime, skill2));
    }

    void FiringRadialProjectiles(Skill3Data skill3)
    {
        // 기본 방향 설정 (예: 오른쪽 방향)
        Vector2 baseDirection = new Vector2(1, 0);

        // 발사할 방향 리스트 초기화
        List<Vector2> directions = new List<Vector2>();
        directions.Add(baseDirection);

        // 기본 방향에서 여러 개의 다른 방향 생성
        for (int i = 1; i < skill3.ObjectCount; i++)
        {
            // 각도를 계산하여 새로운 방향 생성
            float angle = i * skill3.angleToAdd; // 예: 15도씩 차이나게 설정
            Vector2 newDirection = Quaternion.Euler(0, 0, angle) * baseDirection;
            directions.Add(newDirection);
        }

        // 각 방향으로 발사체 발사
        foreach (Vector2 direction in directions)
        {
            // 투사체 생성
            GameObject projectile = Instantiate(skill3.SkillPrefab, transform.position, Quaternion.identity);

            // 투사체의 회전값 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // 정규화된 방향으로 투사체 발사
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * skill3.ObjectSpeed;

            // 투사체에 컴포넌트를 추가하고 데미지 설정한 값을 넣어줌
            var projectileScript = projectile.AddComponent<FiringRadialProjectileScript>();
            projectileScript.Damage = skill3.Damage;
        }
    }
    void InitializeSkillDataDictionary()
    {
        SkillDataDictionary = new Dictionary<State, SkillData>
        {
            { State.Skill1, skill1Data },
            { State.Skill2, skill2Data },
            { State.Skill3, skill3Data },
            { State.Skill4, skill4Data },
            { State.Skill5, skill5Data }
        };
    }

    IEnumerator Skill4ExecuteAfterDelay(Skill4Data skill4)
    {
        skill4.Skill4End = false;
        directiontoPlayer();
        skill4Script.animatorSpeed = skill4.ObjectSpeed;
        skill4Script.SkillretentionTime = skill4.SkillretentionTime;
        skill4Script.Damage = skill4.Damage;
        skill4Script.direction = directiontoplayer;
        skill4Script.Skill4Start();
        yield return new WaitUntil(() => skill4.Skill4End); // 스킬이 끝날 때 까지 기다림
        StartCoroutine(SkillEndDelay(Delay_after_using_a_skill, skill4));
    }

    IEnumerator Skill5ExecuteAfterDelay(Skill5Data skill5)
    {
        skill5.Skill5End = false;
        skill5Script.animationSpeed = skill5.ObjectSpeed;
        skill5Script.SkillretentionTime = skill5.SkillretentionTime;
        skill5Script.Damage = skill5.Damage;
        skill5Script.Skill5Start();
        yield return new WaitUntil(() => skill5.Skill5End);
        StartCoroutine(SkillEndDelay(Delay_after_using_a_skill, skill5));
    }

    public void directiontoPlayer()
    {
        directiontoplayer = Player.transform.position - transform.position;
    }
}
