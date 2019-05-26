using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltCollision : MonoBehaviour
{
    public int damage = 2;

    bool isRotate = false;
    bool isVelocity = false;
    
    Transform player;
    float timebeet = 0.3f;
    Vector3 target;
    Vector2 seltransofrm;
    Vector3 movementVector;
    private Vector3 direction;
    [SerializeField] float projectileSpeed = 1f;
    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
        //   target = new Vector2(player.transform.position.x, player.transform.position.y);
        target = player.transform.position;
         transform.Rotate(0.0f, 0.0f, Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg);
       // transform.rotation = Quaternion.LookRotation(player.position);
       // Destroy(gameObject, 5f);
       direction = (player.transform.position - transform.position).normalized;
        seltransofrm = transform.position;
        movementVector = (target - transform.position).normalized * projectileSpeed;
    }
    private void Update()
    {
        //  transform.position = Vector2.MoveTowards(transform.position,
        // target , projectileSpeed * Time.deltaTime);
        //transform.position += direction * projectileSpeed  * Time.deltaTime;

        //gameObject.GetComponent<Rigidbody2D>().velocity = target * 1;
        transform.position += movementVector * Time.deltaTime;
        Vector2 dir = player.transform.position - transform.position;
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        while (timebeet > 0) {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            timebeet -= Time.deltaTime;
        }
      
     


        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().ChangeHealth(damage);
        }
        Destroy(gameObject);
    }


}
