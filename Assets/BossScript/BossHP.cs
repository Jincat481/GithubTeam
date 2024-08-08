using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [System.Serializable]
    public class bossHpData
    {
        public float bossMaxHp;
        [ReadOnly]
        public float bossCurrentHp;

        // 값이 변경될 때 호출되는 메서드
        public void OnValidate() {
            bossCurrentHp = bossMaxHp;
        }
    }
    public GameObject portal;
    public bossHpData bossData;

    // Start is called before the first frame update
    void Start()
    {
        bossData.bossCurrentHp = bossData.bossMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossData.bossCurrentHp <= 0){
            portal.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float Damage){
        float damagePercentage = (Damage / bossData.bossMaxHp) * 100f;
        bossData.bossCurrentHp -= damagePercentage;

        Debug.Log("보스 현재 체력: "+bossData.bossCurrentHp);
    }
}
