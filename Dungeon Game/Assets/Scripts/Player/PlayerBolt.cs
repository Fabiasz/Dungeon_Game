using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerBolt : MonoBehaviour
{
    public int damage = 1;
    AudioSource audioData;
    void Start()
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
           

        }
        Destroy(this.gameObject);
    }

}
