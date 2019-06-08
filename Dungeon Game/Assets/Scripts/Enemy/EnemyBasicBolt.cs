using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBolt : MonoBehaviour
{
    AudioSource audioData;
    public int damage = 2;
    void Start()
    {
        audioData = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioData.Play(0);
            collision.gameObject.GetComponent<PlayerStats>().ChangeHealth(damage);
            
        }
        Destroy(gameObject);
    }
}
