using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltCollision : MonoBehaviour
{
    bool isRotate = false;
    bool isVelocity = false;
    Transform player;
    Vector2 target;
    [SerializeField] float projectileSpeed = 1f;
    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.Rotate(0.0f, 0.0f, Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg);
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
         target , projectileSpeed * Time.deltaTime);
        if(new Vector2(transform.position.x , transform.position.y ) == target)
        {
            GetComponent<Rigidbody2D>().velocity = target * 10;
        }


        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
