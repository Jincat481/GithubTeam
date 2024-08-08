using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    PlayerController playerController;
    public float addHp;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerController.curHp += addHp;
            Debug.Log("플레이어 HP: "+ playerController.curHp);
            Destroy(gameObject);
        }
    }
}
