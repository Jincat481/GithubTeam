using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동, 대쉬, 공격 관련 변수
    public float moveSpeed; // 이동 속도 
    public float dashSpeed; // 대쉬 속도
    public float dashTime; // 대쉬 지속 시간
    public float dashCooldown; // 대쉬 쿨타임
    public float currentdashcooldown; // 현재 대쉬 재사용 대기 시간
    public float dashDamage = 10f; // 대쉬 데미지
    public float dashDistance; // 대쉬 거리
    public bool isDashing; // 대쉬 상태 여부

    // 체력 및 무적 상태 관련 변수
    public float maxHp = 100;
    [ReadOnly]
    public float curHp = 100;
    private bool isInvincible = false; // 무적 상태 여부
    private float invincibleDuration = 0.5f; // 무적 지속 시간

    // 컴포넌트 참조 변수
    private Rigidbody2D MyRigidbody2D;
    private Animator MyAnimator;

    // 근접 공격 관련 변수
    [SerializeField] private float meleeSpeed; // 공격 속도
    [SerializeField] private float damage; // 공격력
    [SerializeField] private float attackDelay; // 공격 딜레이
    private float timeUntimelee; // 공격 쿨타임
    private Vector2 attackDirection; // 공격 방향
    private bool canAttack = true; // 공격 가능 여부
    private bool cooldownReduced = false; // 쿨타임 감소 여부

    void Start()
    {
        // 초기 체력 설정
        curHp = maxHp;

        // 컴포넌트 초기화
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 이동 처리 (대쉬 중이 아닐 때만)
        if (!isDashing)
        {
            HandleMovement();
        }

        // 대쉬 처리
        HandleDash();
        if (currentdashcooldown > 0)
        {
            currentdashcooldown -= Time.deltaTime;
        }

        // 공격 처리
        HandleAttack();
    }

    // 이동 처리 함수
    private void HandleMovement()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // 키 입력에 따라 이동 방향 설정
        if (Input.GetKey(KeyCode.W)) moveVertical = 1; // 위쪽 이동
        if (Input.GetKey(KeyCode.S)) moveVertical = -1; // 아래쪽 이동
        if (Input.GetKey(KeyCode.A)) moveHorizontal = -1; // 왼쪽 이동
        if (Input.GetKey(KeyCode.D)) moveHorizontal = 1; // 오른쪽 이동

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed;
        MyRigidbody2D.velocity = new Vector2(movement.x, movement.y);

        // 이동 애니메이션 처리
        if (movement != Vector2.zero)
        {
            MyAnimator.SetBool("Walk", true);
        }
        else
        {
            MyAnimator.SetBool("Walk", false);
        }
    }

    // 데미지 처리 함수
    public void TakeDamage(float damage)
    {
        if (!isInvincible && curHp > 0)
        {
            curHp -= damage;
            Debug.Log("현재 플레이어 체력 : " + curHp);
            if (curHp < 0)
            {
                curHp = 0;
            }

            StartCoroutine(InvincibilityCoroutine());
        }
    }

    // 무적 상태 코루틴
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Debug.Log("무적 상태중");
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    // 대쉬 처리 함수
    private void HandleDash()
    {
        if (Input.GetMouseButtonDown(1) && !isDashing && currentdashcooldown <= 0)
        {
            StartCoroutine(Dash());
        }
    }

    // 대쉬 코루틴
    private IEnumerator Dash()
    {
        isDashing = true; // 대쉬 상태로 설정

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        currentdashcooldown = dashCooldown;
        float elapsedTime = 0;
        float dashDistanceTraveled = 0;

        while (elapsedTime < dashTime && dashDistanceTraveled < dashDistance)
        {
            float distanceThisFrame = dashSpeed * Time.deltaTime;
            transform.Translate(dashDirection * distanceThisFrame);
            elapsedTime += Time.deltaTime;
            dashDistanceTraveled += distanceThisFrame;

            yield return null;
        }

        isDashing = false; // 대쉬 상태 해제
    }

    // 충돌 처리 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            ApplyDashDamage(collision.collider);
        }
    }

    private void ApplyDashDamage(Collider2D collider)
    {
        EnemyMove enemyMove = collider.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(dashDamage); // 대쉬 데미지 적용
        }

        BossHP boss = collider.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(dashDamage); // 보스에게 대쉬 데미지 적용
        }

        RangedMonster rangedMonster = collider.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(dashDamage); // RangedMonster에게 대쉬 데미지 적용
        }
    }



    // 공격 처리 함수
    private void HandleAttack()
    {
        if (timeUntimelee <= 0f && canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                attackDirection = (mousePosition - (Vector2)transform.position).normalized;

                // 공격 애니메이션 실행
                MyAnimator.SetTrigger("Attack");

                // 공격 딜레이 시작
                StartCoroutine(AttackDelayCoroutine());

                timeUntimelee = meleeSpeed; // 쿨타임 초기화
                cooldownReduced = false; // 쿨다운 감소 플래그 초기화
            }
        }
        else
        {
            timeUntimelee -= Time.deltaTime; // 쿨타임 감소
        }
    }

    // 공격 딜레이 코루틴
    private IEnumerator AttackDelayCoroutine()
    {
        canAttack = false; // 공격 불가능 상태로 설정
        yield return new WaitForSeconds(attackDelay); // 공격 딜레이 대기
        canAttack = true; // 공격 가능 상태로 설정
    }

    // 트리거 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other);
        }
        else if (other.CompareTag("Boss")) // 보스 태그 확인
        {
            HandleBossCollision(other);
        }
    }

    private void HandleEnemyCollision(Collider2D other)
    {
        EnemyMove enemyMove = other.GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.TakeDamage(damage); // 적에게 데미지를 줍니다.
            Debug.Log("공격 성공; 방향: " + attackDirection); // 공격 성공 메시지와 방향 출력

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }

        RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
        if (rangedMonster != null)
        {
            rangedMonster.TakeDamage(damage); // 적에게 데미지를 줍니다.
            Debug.Log("공격 성공; 방향: " + attackDirection); // 공격 성공 메시지와 방향 출력

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }
    }

    private void HandleBossCollision(Collider2D other)
    {
        BossHP boss = other.GetComponent<BossHP>();
        if (boss != null)
        {
            boss.TakeDamage(damage); // 보스에게 데미지를 줍니다.
            Debug.Log("보스 공격 성공; 방향: " + attackDirection); // 보스 공격 성공 메시지와 방향 출력

            ReduceDashCooldown(); // 대쉬 쿨타임 감소
        }
    }

    private void ReduceDashCooldown()
    {
        if (!cooldownReduced)
        {
            currentdashcooldown -= 1f;
            if (currentdashcooldown < 0)
            {
                currentdashcooldown = 0;
            }
            cooldownReduced = true; // 쿨다운 감소가 한 번만 일어나도록 설정
        }
    }

}

