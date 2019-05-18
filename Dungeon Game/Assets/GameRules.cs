﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public int NPCS=0;
    public GameObject  portalObject;
    void Update()
    {
        EnemyCounter();
         VictoryLoading();
    }

   void VictoryLoading()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        if (NPCS == 0 && portals.Length == 0 )
        {

            //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Instantiate(portalObject);
            NPCS = 1;
        }
    }

    private  void EnemyCounter()
    {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        NPCS = enemies.Length;
       
    }
    
}