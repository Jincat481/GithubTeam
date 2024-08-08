using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    private ParentCollider parentCollider;

    // Start is called before the first frame update
    void Start()
    {
        parentCollider = GetComponentInParent<ParentCollider>();

        transform.rotation = transform.parent.rotation;
        // Transform parentTransform = transform.parent;

        // if(parentTransform != null)
        // {
        //     // 부모의 회전 값 가져오기
        //     Quaternion parentRotation = parentTransform.rotation;

        //     // 부모의 회전 값을 eulerAngles로 변환하여 출력하기
        //     Vector3 parentEulerAngles = parentRotation.eulerAngles;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.CompareTag("Player"))
        {
            parentCollider.OnPlayerCollision();
        }
    }
}
