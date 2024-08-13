using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동, 대쉬 관련 변수
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
    public float curHp = 100;
    private bool isInvincible = false; // 무적 상태 여부
    private float invincibleDuration = 0.5f; // 무적 지속 시간

    
    private Rigidbody2D MyRigidbody2D;
    private Animator MyAnimator;

    // 이펙트 및 사운드 관련 변수
    public PlayerEffects playerEffects; // 플레이어 이펙트 스크립트
    public PlayerAttack playerAttack; // 플레이어 어택 스크립트

    private void Start()
    {
        // 초기 체력 설정
        curHp = maxHp;

        // 컴포넌트 초기화
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();

        // 이펙트 및 공격 스크립트 설정
        if (playerEffects == null)
        {
            playerEffects = GetComponent<PlayerEffects>();
        }

        if (playerAttack == null)
        {
            playerAttack = GetComponent<PlayerAttack>();
        }
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

        // 쿨타임 감소
        if (currentdashcooldown > 0)
        {
            currentdashcooldown -= Time.deltaTime;
        }

   
        
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

            // 데미지 사운드 재생
            SoundManager.Instance.Play("Damage");

            // 데미지 이펙트 재생
            if (playerEffects != null)
            {
                playerEffects.PlayDamageEffect();
            }
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

        // 대쉬 사운드 재생
        SoundManager.Instance.Play("Dash");

        // 대쉬 이펙트 재생
        if (playerEffects != null)
        {
            playerEffects.PlayDashEffect();
        }

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

    private void Update()
    {
        // 공격 입력 처리
        if (Input.GetMouseButtonDown(0) && playerAttack != null)
        {
            playerAttack.PerformAttack(); // 근접 공격 호출
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 대쉬 상태일 때 보스와 충돌 시 대쉬 대미지만 적용
        if (isDashing && other.CompareTag("Boss"))
        {
            ApplyDashDamage(other); // 대쉬 대미지 적용
        }
    }

    // 대쉬 대미지 적용 함수
    private void ApplyDashDamage(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            BossHP boss = other.GetComponent<BossHP>();
            if (boss != null)
            {
                boss.TakeDamage(dashDamage); // 대쉬 대미지 적용
                Debug.Log("대쉬 공격 성공");
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            if (enemyMove != null)
            {
                enemyMove.TakeDamage(dashDamage); // 대쉬 대미지 적용
                Debug.Log("대쉬 공격 성공");
            }

            RangedMonster rangedMonster = other.GetComponent<RangedMonster>();
            if (rangedMonster != null)
            {
                rangedMonster.TakeDamage(dashDamage); // 대쉬 대미지 적용
                Debug.Log("대쉬 공격 성공");
            }
        }

        // 보스에게 대쉬 대미지를 적용해도 쿨타임을 초기화하지 않음
        if (!other.CompareTag("Boss"))
        {
            currentdashcooldown = 0; // 대쉬 쿨타임 초기화
        }
    }
}
