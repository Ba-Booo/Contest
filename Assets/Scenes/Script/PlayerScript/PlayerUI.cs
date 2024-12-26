using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    int playerMaxHP;
    public int playerNowHP;

    PlayerMove ultimateGauge;

    public Slider hpSlider;
    public Image ultimateSlider;

    void Start()
    {

        ultimateGauge = GetComponent<PlayerMove>();

        playerNowHP = playerMaxHP;
        hpSlider.maxValue = playerMaxHP;

    }

    void Update()
    {

        hpSlider.value = playerNowHP;
        ultimateSlider.fillAmount = Mathf.Lerp( ultimateSlider.fillAmount, ultimateGauge.nowUltimateAttackGauge / ultimateGauge.maxUltimateAttackGauge, 3f * Time.deltaTime );

    }

}
