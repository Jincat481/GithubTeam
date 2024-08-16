using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsCollisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject o)
    {
        if(o.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
