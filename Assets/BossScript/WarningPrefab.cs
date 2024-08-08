using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPrefab : MonoBehaviour
{
    public float warningTime;
    private float elapsedTime; // 경과 시간을 저장할 변수
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f; // 시작 시 경과 시간을 0으로 초기화
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 경과 시간을 업데이트
        elapsedTime += Time.deltaTime;

        // 경과 시간이 경고 시간 값과 크거나 같으면
        if(elapsedTime >= warningTime)
        {
            Destroy(gameObject);
        }
    }
}
