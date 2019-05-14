﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        health = maxHealth;
        mana = maxMana;
    }

    private void FixedUpdate()
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
        throw new NotImplementedException();
    }
}