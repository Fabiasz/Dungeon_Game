using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltCollision : MonoBehaviour
{
    bool isRotate = false;
    GameObject player;
    Transform target;
    [SerializeField] float projectileSpeed = 1f;
    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //Physics2D.IgnoreLayerCollision(12, 10);
        
        target = player.transform;
        if (isRotate)
        {
            transform.Rotate(0.0f, 0.0f, Mathf.Atan2(target.position.y, target.position.x) * Mathf.Rad2Deg);
            isRotate = true;
        }

       // GetComponent<Rigidbody2D>().velocity = projectileSpeed*5 * Time.deltaTime * target.position;
        Debug.Log(target.position.ToString());
        transform.position = Vector2.MoveTowards(transform.position,
         target.position, projectileSpeed * Time.deltaTime);

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
