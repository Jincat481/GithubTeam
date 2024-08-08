using System.Collections;
using UnityEngine;

public class Pattern3 : MonoBehaviour
{
    private Weapon weaponScript;
    public GameObject projectilePrefab;
    public float minX = 10.5f;
    public float maxX = 21.7f;
    public float minY = 0.33f;
    public float maxY = 10.66f;
    public float moveSpeed = 7f;
    public float duration = 10f;
    private GameObject projectileInstance;
    private Vector3 randomPosition;
    private bool patternCoroutineRunning = false;
    private GameObject player;
    private Enemy enemy;
    Rigidbody2D rb;
    public enum State
    {
        WeaponPositionMove,
        Idle,
        RandomPosition,
        Pattern3ProjectileMove,
        Pattern3Running,
        CreateProjectiles
    }
    public State stateCheck;

    void Start()
    {
        stateCheck = State.Idle;
        weaponScript = GetComponent<Weapon>();
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Player GameObject does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is not found.");
        }

        if (weaponScript == null)
        {
            Debug.LogError("Weapon component is not found on this GameObject.");
        }
    }

    void Update()
    {
        switch (stateCheck)
        {
            case State.WeaponPositionMove:
                HandleWeaponPositionMove();
                break;
            case State.Idle:
                Idle();
                break;
            case State.RandomPosition:
                HandleRandomPosition();
                break;
            case State.Pattern3ProjectileMove:
                HandlePattern3ProjectileMove();
                break;
            case State.CreateProjectiles:
                HandleCreateProjectiles();
                break;
            case State.Pattern3Running:
                HandlePattern3Running();
                break;
        }
    }

    private void HandleWeaponPositionMove()
    {
        if (Vector3.Distance(weaponScript.transform.position, weaponScript.Weapon_Position.transform.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(weaponScript.transform.position, weaponScript.Weapon_Position.transform.position, Time.deltaTime * moveSpeed);
        }
        if (Vector3.Distance(weaponScript.transform.position, weaponScript.Weapon_Position.transform.position) < 0.3f)
        {
            weaponScript.Projectiles_Delay = -1f;
            stateCheck = State.Idle;
        }
    }

    public void SetStateRandomPosition()
    {
        stateCheck = State.RandomPosition;
    }

    private void Idle(){

    }
    private void HandleRandomPosition()
    {
        randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        weaponScript.Projectiles_Delay = 30f;
        stateCheck = State.Pattern3ProjectileMove;
    }

    private void HandlePattern3ProjectileMove()
    {
        if (Vector3.Distance(weaponScript.transform.position, randomPosition) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(weaponScript.transform.position, randomPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            stateCheck = State.CreateProjectiles;
        }
    }

    private void HandleCreateProjectiles()
    {
        CreateProjectiles(randomPosition);
        stateCheck = State.Pattern3Running;
    }

    private void HandlePattern3Running()
    {
        if (!patternCoroutineRunning)
        {
            StartCoroutine(Pattern3StartCorutine());
        }
    }

    private void CreateProjectiles(Vector3 randomPosition)
    {
        DisableWeapon(); // 무기를 비활성화
        projectileInstance = Instantiate(projectilePrefab, randomPosition, Quaternion.identity); // 패턴 3 투사체 생성
    }

    private void DisableWeapon()
    {
        // 무기의 렌더러와 모든 콜라이더를 비활성화하여 무기를 숨김
        var renderers = weaponScript.GetComponents<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        var colliders = weaponScript.GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void EnableWeapon()
    {
        // 무기의 렌더러를 활성화하여 무기를 다시 보이게 함
        var renderers = weaponScript.GetComponents<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    private IEnumerator Pattern3StartCorutine()
    {
        patternCoroutineRunning = true;
        yield return new WaitForSeconds(duration); // 지속 시간동안 대기
        EnableWeapon(); // 무기를 다시 활성화
        Destroy(projectileInstance);
        rb.velocity = Vector2.zero; // 속도를 0으로 수정해야함
        stateCheck = State.WeaponPositionMove;
        patternCoroutineRunning = false;
    }
}
