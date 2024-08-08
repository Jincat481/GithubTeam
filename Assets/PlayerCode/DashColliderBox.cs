using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // 콜라이더를 저장할 변수 이름 변경

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // 콜라이더 컴포넌트 가져오기
        dashCollider.enabled = false; // 시작할 때 콜라이더 비활성화
    }

    void Update()
    {
        // 데쉬 상태일 때만 콜라이더 활성화
        dashCollider.enabled = controller.isDashing; // isDashing 변수를 사용하여 상태 확인
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            controller.currentdashcooldown = 0;
        }
        else if (other.CompareTag("Boss"))
        {
            // 보스와의 충돌 처리 로직 추가 가능
        }
    }
}
