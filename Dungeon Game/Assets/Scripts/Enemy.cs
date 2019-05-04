using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     float timebeetweenShots = 0f;
    [SerializeField] float starttimebeetweenShots;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] GameObject boltPrefab;
    Transform player;
    Transform target;
    private void Start()
    {
        
      
    }
    private void Update()
    {
        Shooting();
    }
   public void Shooting()
    {
        
        if(timebeetweenShots <= 0)
        {
            Debug.Log("time");
            Instantiate(boltPrefab, transform.position, Quaternion.identity);
             timebeetweenShots = starttimebeetweenShots;
           
        }
        else
        {
            timebeetweenShots -= Time.deltaTime;
        }
    }
}
