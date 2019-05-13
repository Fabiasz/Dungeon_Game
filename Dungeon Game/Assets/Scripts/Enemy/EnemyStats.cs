using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 5;
    public float movementSpeed = 2;
    public int touchDamage = 1;
    public float touchKnockback = 5;

    [HideInInspector]
    public int health;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            PlayerStats stats = col.gameObject.GetComponent<PlayerStats>();
            stats.health -= touchDamage;
            Debug.Log(stats.health);
            Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(playerRb.position.x - rigidbody.position.x, playerRb.position.y - rigidbody.position.y);
            playerRb.velocity += force.normalized * touchKnockback;
        }
    }

}
