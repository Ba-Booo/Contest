using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    int playerMaxHP;
    public int playerNowHP;

    public Slider hpSlider;

    void Start()
    {
        playerNowHP = playerMaxHP;
        hpSlider.maxValue = playerMaxHP;
    }

    void Update()
    {
        hpSlider.value = playerNowHP;
    }

}
