using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector2 center; // ī�޶� �߾� ��ġ
    public Vector2 size;   // ī�޶� ������ �� �ִ� ������ ũ��
    float height;
    float width;

    public float gridSpacing = 1.0f; // �׸��� ����

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
        DrawGrid();
    }

    void DrawGrid()
    {
        Gizmos.color = Color.green;

        float minX = center.x - size.x * 0.5f;
        float maxX = center.x + size.x * 0.5f;
        float minY = center.y - size.y * 0.5f;
        float maxY = center.y + size.y * 0.5f;

        
        for (float x = minX; x <= maxX; x += gridSpacing)
        {
            Gizmos.DrawLine(new Vector3(x, minY, 0), new Vector3(x, maxY, 0));
        }

        
        for (float y = minY; y <= maxY; y += gridSpacing)
        {
            Gizmos.DrawLine(new Vector3(minX, y, 0), new Vector3(maxX, y, 0));
        }
    }

    void LateUpdate()
    {
        // Ÿ���� ��ġ�� �ε巴�� �̵�
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        // ī�޶� �̵��� �� �ִ� ���� ���
        float halfWidth = width;
        float halfHeight = height;

        float minX = center.x - size.x * 0.5f + halfWidth;
        float maxX = center.x + size.x * 0.5f - halfWidth;

        float minY = center.y - size.y * 0.5f + halfHeight;
        float maxY = center.y + size.y * 0.5f - halfHeight;

        // ī�޶��� ��ġ�� ���� ���� ����
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        // ���ѵ� ��ġ�� ī�޶� �̵�
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
