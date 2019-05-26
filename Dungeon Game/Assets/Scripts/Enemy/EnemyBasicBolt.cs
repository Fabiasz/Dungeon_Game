using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBolt : MonoBehaviour
{
    public int damage = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().ChangeHealth(damage);
        }
        Destroy(gameObject);
    }
}
