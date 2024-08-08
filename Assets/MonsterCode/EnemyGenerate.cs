using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject Enemyprf;
    public float spawnInterval = 2f; // �� ���� ����
    private int enemyCount = 0; // ������ ���� ��
    private int maxEnemyCount = 3; // �ִ� ������ ���� ��

    // Start is called before the first frame update
    void Start()
    {
        // �ֱ������� ���� ����
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
            // �ִ� �� ���� �����ϸ� �� ���� ����
            CancelInvoke("SpawnEnemy");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
