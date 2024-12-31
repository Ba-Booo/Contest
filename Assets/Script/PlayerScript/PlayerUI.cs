using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] 
    bool tutorial;

    public int playerMaxHP;
    public int playerNowHP;

    PlayerMove playerGauge;

    [SerializeField] Slider hpSlider;
    [SerializeField] Image ultimateSlider;
    [SerializeField] Image dashSlider;

    void Start()
    {

        playerGauge = GetComponent<PlayerMove>();

        if( !tutorial )
        {
            playerNowHP = playerMaxHP;
        }
        hpSlider.maxValue = playerMaxHP;

    }

    void Update()
    {

        hpSlider.value = playerNowHP;
        ultimateSlider.fillAmount = Mathf.Lerp( ultimateSlider.fillAmount, playerGauge.nowUltimateAttackGauge / playerGauge.maxUltimateAttackGauge, 3f * Time.deltaTime );
        dashSlider.fillAmount = playerGauge.nowSlowMotionGauge / playerGauge.maxSlowMotionGauge;

    }

}
