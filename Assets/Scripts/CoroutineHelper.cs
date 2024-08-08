using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{ // 코루틴을 시작할 수 있게 해주는 헬퍼 클래스
    public static CoroutineHelper Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
