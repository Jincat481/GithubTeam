using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector2 center; // 카메라 중앙 위치
    public Vector2 size;   // 카메라가 움직일 수 있는 영역의 크기
    float height;
    float width;

    public float gridSpacing = 1.0f; // 그리드 간격

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
        // 타겟의 위치로 부드럽게 이동
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        // 카메라가 이동할 수 있는 범위 계산
        float halfWidth = width;
        float halfHeight = height;

        float minX = center.x - size.x * 0.5f + halfWidth;
        float maxX = center.x + size.x * 0.5f - halfWidth;

        float minY = center.y - size.y * 0.5f + halfHeight;
        float maxY = center.y + size.y * 0.5f - halfHeight;

        // 카메라의 위치를 범위 내로 제한
        float clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(targetPosition.y, minY, maxY);

        // 제한된 위치로 카메라 이동
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
