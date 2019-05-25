using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private Image healthBar;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = (float)playerStats.health / (float) playerStats.maxHealth;
    }
}
