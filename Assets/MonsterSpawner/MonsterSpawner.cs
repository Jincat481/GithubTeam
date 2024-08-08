using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float spawnTimer;

    [ReadOnly]
    public float currentTimer;
    public int maximumRangedMonstercount;

    [System.Serializable]
    public class MonsterType
    {
        public GameObject meleeMonster;
        public GameObject rangedMonster;
    }

    [System.Serializable]
    public class SpawnerData
    {
        public Transform spawnPoint;
    }

    public MonsterType monsterType;
    public SpawnerData[] spawnerDatas;
    private int rangedmonsterCount = 0;

    void Start()
    {

        currentTimer = spawnTimer;
    }

    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            foreach (var spawnerData in spawnerDatas)
            {
                SpawnMonster(spawnerData);
            }
            
            currentTimer = spawnTimer;

            // 스폰 사이클이 완료된 후 rangedmonsterCount 초기화
            rangedmonsterCount = 0;
        }
    }

    public void SpawnMonster(SpawnerData spawnerData)
    {
        GameObject selectedMonster;
        if (rangedmonsterCount < maximumRangedMonstercount)
        {
            // 랜덤으로 근접 또는 원거리 몬스터를 선택
            selectedMonster = (Random.Range(0, 2) == 0) ? monsterType.meleeMonster : monsterType.rangedMonster;

            if (selectedMonster == monsterType.rangedMonster)
            {
                rangedmonsterCount++;
            }
        }
        else
        {
            selectedMonster = monsterType.meleeMonster;
        }
        // 선택된 스폰 포인트에서 몬스터를 스폰
        Instantiate(selectedMonster, spawnerData.spawnPoint.position, Quaternion.identity);
    }
}
