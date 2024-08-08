using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject Enemyprf;
    public float spawnInterval = 2f; // 적 생성 간격
    private int enemyCount = 0; // 생성된 적의 수
    private int maxEnemyCount = 3; // 최대 생성할 적의 수

    // Start is called before the first frame update
    void Start()
    {
        // 주기적으로 적을 생성
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyCount < maxEnemyCount)
        {
            GameObject Enemy = Instantiate(Enemyprf) as GameObject;
            Enemy.transform.position = new Vector2(Random.Range(-5.5f, 7.3f), -3.55f);
            enemyCount++;
        }
        else
        {
            // 최대 적 수에 도달하면 적 생성 중지
            CancelInvoke("SpawnEnemy");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
