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

    // 대쉬 방향 저장
    public Vector2 dashDirection;

    // 체력 및 무적 상태 관련 변수
    public float maxHp = 100;
    public float curHp = 100;
    private bool isInvincible = false; // 무적 상태 여부
    private float invincibleDuration = 0.5f; // 무적 지속 시간

    private Rigidbody2D MyRigidbody2D;
    private Animator MyAnimator;
    private SpriteRenderer MySpriteRenderer; // 스프라이트 렌더러

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
        MySpriteRenderer = GetComponent<SpriteRenderer>();

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

    private void FixedUpdate()
    {
        // 대쉬 중이 아닐 때만 이동 처리
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
        // 공격 중일 때는 이동 애니메이션을 멈춤
        if (MyAnimator.GetBool("IsAttacking"))
            return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed;
        MyRigidbody2D.velocity = movement;

        // 이동 애니메이션 처리
        if (!isDashing)
        {
            bool isChange = false; // IsChange 플래그 초기화

            // MoveX 값이 변경되었는지 확인하고, 변경되었으면 IsChange 플래그 설정
            if (MyAnimator.GetFloat("MoveX") != moveHorizontal)
            {
                MyAnimator.SetFloat("MoveX", moveHorizontal);
                isChange = true;
            }

            // MoveY 값이 변경되었는지 확인하고, 변경되었으면 IsChange 플래그 설정
            if (MyAnimator.GetFloat("MoveY") != moveVertical)
            {
                MyAnimator.SetFloat("MoveY", moveVertical);
                isChange = true;
            }

            // IsChange 값 업데이트
            MyAnimator.SetBool("IsChange", isChange);

            MyAnimator.SetBool("IsMoving", movement.sqrMagnitude > 0);
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
        isDashing = true;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        // 대쉬 방향에 따른 애니메이션 설정
        if (dashDirection.x > 0)
        {
            MyAnimator.SetBool("IsRightDashing", true);
        }
        else
        {
            MyAnimator.SetBool("IsLeftDashing", true);
        }

        // 대쉬 중에는 이동 애니메이션을 멈춤
        MyAnimator.SetBool("IsMoving", false);

        currentdashcooldown = dashCooldown;
        float elapsedTime = 0;
        float dashDistanceTraveled = 0;

        // 대쉬 사운드 재생
        SoundManager.Instance.Play("Dash");

        // 대쉬 이펙트 재생
        if (playerEffects != null)
        {
            if (dashDirection.x > 0)
            {
                playerEffects.PlayRightDashEffect();
            }
            else
            {
                playerEffects.PlayLeftDashEffect();
            }
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

        // 대쉬 애니메이션 종료
        MyAnimator.SetBool("IsRightDashing", false);
        MyAnimator.SetBool("IsLeftDashing", false);

        // 대쉬 후 이동 상태 재확인하여 걷기 애니메이션 재활성화
        HandleMovement();
    }

    private void Update()
    {
        // 공격 입력 처리
        if (Input.GetMouseButtonDown(0) && playerAttack != null)
        {
            StartCoroutine(PerformAttack());
        }
    }

    // 공격 애니메이션 및 행동 처리 코루틴
    private IEnumerator PerformAttack()
    {
        MyAnimator.SetBool("IsAttacking", true); // 공격 상태 활성화
        playerAttack.PerformAttack(); // 공격 실행  
        MyAnimator.SetTrigger("Attack"); // 공격 애니메이션 트리거

        yield return new WaitForSeconds(0.3f); // 공격 애니메이션 시간 (적절히 조정 필요)

        MyAnimator.SetBool("IsAttacking", false); // 공격 상태 해제

        // 공격 후 이동 상태 및 애니메이션 재확인
        HandleMovement();
    }
}
