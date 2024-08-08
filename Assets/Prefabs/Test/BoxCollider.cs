using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    private ParentCollider parentCollider;
    // Start is called before the first frame update
    void Start()
    {
        parentCollider = GetComponentInParent<ParentCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.CompareTag("Wall"))
        {
            parentCollider.OnWallsCollision();
        }
    }
}
