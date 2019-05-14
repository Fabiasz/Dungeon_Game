using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : MonoBehaviour
{
    public int damage = 1;
    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            EnemyStats stats = col.gameObject.GetComponent<EnemyStats>();
            stats.ChangeHealth(damage);
        }
        Destroy(this.gameObject);
    }
}
