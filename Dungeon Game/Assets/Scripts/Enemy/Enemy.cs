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
    GameObject rotator;
    public int rotSpeed = 20000;
    public float distance = 200;
  
    private void Start()
    {
        //  rotator = gameObject.transform.GetChild(0).gameObject;
        Physics2D.queriesStartInColliders = false;

    }
    private void Update()
    {
        //Shooting();
        //CheckIfPlayerInSightArea();
    }
   public void Shooting()
    {
        
     //   if(timebeetweenShots <= 0)
       // {
            Debug.Log("time");
            Instantiate(boltPrefab, transform.position, Quaternion.identity);
        //    timebeetweenShots = starttimebeetweenShots;
           
       // }
        //else
        //{
            timebeetweenShots -= Time.deltaTime;
        //}
    }
    public void CheckIfPlayerInSightArea()
    {
        

        /* var collider = GetComponent<CapsuleCollider2D>();
         ContactFilter2D filter = new ContactFilter2D()
         {
         };
         RaycastHit2D[] hits = new RaycastHit2D[10];
         float distance = 100f;
         int numHits = collider.Cast(Vector2.right, filter, hits, distance);
         for(int i = 0; i < numHits; i++)
         {
             Debug.Log(hits[i].collider.tag);
             if(hits[i].collider.tag == "Player" )
             {
                 Shooting();
             }
         }*/
        // Collider Castr instead of rayCast

    } 
}
