using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBouncingBolt : PlayerBolt
{
    AudioSource audioData;
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        audioData.Play(0);
        if (col.gameObject.tag == "Enemy")
        {
            EnemyStats stats = col.gameObject.GetComponent<EnemyStats>();
            stats.ChangeHealth(damage);
            Destroy(this.gameObject);
        }
        //this.GetComponent<Rigidbody2D>().velocity *= 2;
    }
}
