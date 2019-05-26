using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private Image manaBar;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        manaBar = GetComponent<Image>();
    }

    void Update()
    {
        manaBar.fillAmount = (float)playerStats.mana / (float)playerStats.maxMana;
    }
}
