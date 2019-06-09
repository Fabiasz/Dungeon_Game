using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 10;
    [HideInInspector]
    public int health;
    public float maxMana = 100f;
    [HideInInspector]
    public float mana;
    public float manaRegen = 20f;
    public float movementSpeed = 6f;
    AudioSource audioData;

    private void Awake()
    {
        health = maxHealth;
        mana = maxMana;
        audioData = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ManaRegeneration();
        
    }

    private void ManaRegeneration()
    {
        mana += Time.fixedDeltaTime * manaRegen;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }

    // healing is taking negative damage
    public void ChangeHealth(int damage)
    {
        health -= damage;
        audioData.Play(0);
        Debug.Log(health);
        if (health > maxHealth)
        {
            health = maxHealth;
        } else if(health <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        //todo: make real losing scenario
        SceneManager.LoadScene("Failure");
    }
}
