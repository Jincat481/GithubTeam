using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraCtrl : MonoBehaviour
{
    public Transform target;  
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
        transform.position = Vector3 .Lerp(transform.position, target.position, Time.deltaTime * speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y , -10f);
        float Ix = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x , -Ix + center.x , Ix + center.x);

        float Iy = size.x * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -Iy + center.y, Iy + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}