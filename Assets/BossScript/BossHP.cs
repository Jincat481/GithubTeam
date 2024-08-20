using System;
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
    public GameObject soundManger;
    private AudioSource audioSource;
    private MsoundManger hurtaudio;
    private Color originalColor;
    private Color hitColor = Color.red; 
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bossData.bossCurrentHp = bossData.bossMaxHp;
        hurtaudio = soundManger.GetComponent<MsoundManger>();
        audioSource = soundManger.GetComponent<AudioSource>(); // audioSource 초기화
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
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
        StartCoroutine(SpriteColorManger.HitColor(sr, hitColor, originalColor));
        audioSource.PlayOneShot(hurtaudio.BossHurtAudio, hurtaudio.BossHurtVolumeScale);
        Debug.Log("보스 현재 체력: "+bossData.bossCurrentHp);
    }
}
