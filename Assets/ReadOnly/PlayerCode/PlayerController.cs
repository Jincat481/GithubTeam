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

    
    public PlayerEffects playerEffects; // 플레이어 이펙트 스크립트
    public PlayerAttack playerAttack; // 플레이어 어택 스크립트

    private void Start()
    {
        
        curHp = maxHp;

        
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();

        
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

       
        HandleDash();

        
        if (currentdashcooldown > 0)
        {
            currentdashcooldown -= Time.deltaTime;
        }
    }

    // 이동 처리 함수
    private void HandleMovement()
    {
        
        if (MyAnimator.GetBool("IsAttacking"))
            return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed;
        MyRigidbody2D.velocity = movement;

        // 이동 애니메이션 처리
        if (!isDashing)
        {
            bool isChange = false; 

            
            if (MyAnimator.GetFloat("MoveX") != moveHorizontal)
            {
                MyAnimator.SetFloat("MoveX", moveHorizontal);
                isChange = true;
            }

            
            if (MyAnimator.GetFloat("MoveY") != moveVertical)
            {
                MyAnimator.SetFloat("MoveY", moveVertical);
                isChange = true;
            }

           
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

           
            SoundManager.Instance.Play("Damage");

            
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

        if (dashDirection.x > 0)
        {
            MyAnimator.SetBool("IsRightDashing", true);
        }
        else
        {
            MyAnimator.SetBool("IsLeftDashing", true);
        }

        MyAnimator.SetBool("IsMoving", false);

        currentdashcooldown = dashCooldown;
        float elapsedTime = 0;
        float dashDistanceTraveled = 0;

        SoundManager.Instance.Play("Dash");

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

        Rigidbody2D rb = GetComponent<Rigidbody2D>();  

        while (elapsedTime < dashTime && dashDistanceTraveled < dashDistance)
        {
            float distanceThisFrame = dashSpeed * Time.deltaTime;
            Vector2 newPosition = rb.position + dashDirection * distanceThisFrame;  
            rb.MovePosition(newPosition);  

            elapsedTime += Time.deltaTime;
            dashDistanceTraveled += distanceThisFrame;

            yield return null;
        }

        isDashing = false;

        MyAnimator.SetBool("IsRightDashing", false);
        MyAnimator.SetBool("IsLeftDashing", false);

        HandleMovement();
    }


    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && playerAttack != null)
        {
            StartCoroutine(PerformAttack());
        }
    }

    // 공격 애니메이션 및 행동 처리 코루틴
    private IEnumerator PerformAttack()
    {
        MyAnimator.SetBool("IsAttacking", true);
        playerAttack.PerformAttack(); 
        MyAnimator.SetTrigger("Attack"); 

        yield return new WaitForSeconds(0.3f); 

        MyAnimator.SetBool("IsAttacking", false); 

        
        HandleMovement();
    }
}
