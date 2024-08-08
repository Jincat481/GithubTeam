using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    bool isPlayerInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInTrigger && Input.GetKey(KeyCode.W)){
            SceneManager.LoadScene("New Scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        if(o.gameObject.CompareTag("Player")){
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D o) {
        if(o.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
