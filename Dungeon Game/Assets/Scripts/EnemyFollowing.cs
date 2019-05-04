using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float stopDist = 5f;
    [SerializeField] float retreatDist = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if(Vector2.Distance(transform.position , player.position ) > stopDist)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed *  Time.deltaTime);
        }else if(Vector2.Distance(transform.position, player.position) < stopDist   && Vector2.Distance(transform.position, player.position) > retreatDist)
        {
            transform.position = this.transform.position;
        } else if(Vector2.Distance(transform.position, player.position) < retreatDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }
}
