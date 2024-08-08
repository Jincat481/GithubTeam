using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashColliderBox : MonoBehaviour
{
    public PlayerController controller;
    private Collider2D dashCollider; // �ݶ��̴��� ������ ���� �̸� ����

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        dashCollider = GetComponent<Collider2D>(); // �ݶ��̴� ������Ʈ ��������
        dashCollider.enabled = false; // ������ �� �ݶ��̴� ��Ȱ��ȭ
    }

    void Update()
    {
        // ���� ������ ���� �ݶ��̴� Ȱ��ȭ
        dashCollider.enabled = controller.isDashing; // isDashing ������ ����Ͽ� ���� Ȯ��
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            controller.currentdashcooldown = 0;
        }
        else if (other.CompareTag("Boss"))
        {
            // �������� �浹 ó�� ���� �߰� ����
        }
    }
}
