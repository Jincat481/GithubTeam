using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Fields : MonoBehaviour
{
    private PointEffector2D pointEffector;
    private GameObject player;
    public float gravityRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pointEffector = GetComponent<PointEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustGravityEffect();
    }
    
    private void AdjustGravityEffect(){
        if(player != null && pointEffector != null){
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if(distanceToPlayer > gravityRadius){
                // 플레이어가 설정한 중력 범위 밖에 있을 때 비활성화
                pointEffector.enabled = false;
            }
            else{
                // 플레이어가 설정한 중력 범위 안에 있으면 활성화
                pointEffector.enabled = true;
            }
        }
        
    }
}
