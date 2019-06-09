using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShatterBolt : PlayerBolt
{
    public GameObject boltPrefab;
    public float bulletVelocity = 5;
    public float bulletLifeTime = 1.5f;
    private float[] angles;
    public int newBullets = 4;
    private bool ifCollided = false;
    private Vector3 bulletDirection;

    private void Start()
    {
        angles = new float[newBullets];
        for (int i = 0; i < newBullets; i++)
        {
            angles[i] = 360f / newBullets * (i + 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            EnemyStats stats = col.gameObject.GetComponent<EnemyStats>();
            stats.ChangeHealth(damage);
        }
        ifCollided = true;

        Destroy(this.gameObject);
    }
}

