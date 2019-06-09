using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationBullet : PlayerBolt
{
    public float acceleration = 2;
    public float maxSpeed = 20;
    private Rigidbody2D rb;
    AudioSource audioData;
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        if (this.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipY = true;
        }
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = rb.velocity.normalized;
        rb.velocity += Time.fixedDeltaTime * rb.velocity * acceleration;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = direction * maxSpeed;
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        audioData.Play(0);
        if (col.gameObject.tag == "Enemy")
        {
            EnemyStats stats = col.gameObject.GetComponent<EnemyStats>();
            stats.ChangeHealth(damage);
        }
        Destroy(this.gameObject);
        //this.GetComponent<Rigidbody2D>().velocity *= 2;
    }
}
