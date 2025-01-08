using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUITutorial : MonoBehaviour
{


    public int playerMaxHP;
    public int playerNowHP;

    PlayerMoveTutorial playerGauge;

    [SerializeField] Slider hpSlider;
    [SerializeField] Image ultimateSlider;
    [SerializeField] Image dashSlider;

    void Start()
    {

        playerGauge = GetComponent<PlayerMoveTutorial>();
        
        hpSlider.maxValue = playerMaxHP;

    }

    void Update()
    {

        hpSlider.value = playerNowHP;
        ultimateSlider.fillAmount = Mathf.Lerp( ultimateSlider.fillAmount, playerGauge.nowUltimateAttackGauge / playerGauge.maxUltimateAttackGauge, 3f * Time.deltaTime );
        dashSlider.fillAmount = playerGauge.nowSlowMotionGauge / playerGauge.maxSlowMotionGauge;

    }

}
