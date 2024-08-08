using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerMK2 : MonoBehaviour
{
    public bool spawn;
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

    }

    void Update()
    {
        if (spawn)
        {
            foreach (var spawnerData in spawnerDatas)
            {
                SpawnMonster(spawnerData);
            }

            // 스폰 사이클이 완료된 후 rangedmonsterCount 초기화
            rangedmonsterCount = 0;

            spawn = false;
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

