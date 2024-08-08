using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Canvas canvas;
    private GameObject boss;
    private BossHP bossScript;
    private float hpPercent;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        bossScript = boss.GetComponent<BossHP>();
        hpBar.value = bossScript.bossData.bossCurrentHp/ bossScript.bossData.bossMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpPercent = (bossScript.bossData.bossCurrentHp / bossScript.bossData.bossMaxHp) * 100f;

        if(hpPercent <= 0){
            canvas.enabled = false;
        }
        else{
        text.text = "Boss Hp " + hpPercent.ToString("F2") + "%" ;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            bossScript.bossData.bossCurrentHp -= 100f;
        }
        HandleHp();
    }

    private void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value , bossScript.bossData.bossCurrentHp/ bossScript.bossData.bossMaxHp , Time.deltaTime * 5f);
    }
}
