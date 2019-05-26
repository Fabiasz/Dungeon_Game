using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootingEnemy : MonoBehaviour
{
    private RayCastSimple rayCast;
    private EnemyStats stats;
    public float minPlayerDistance = 1, maxPlayerDistance = 5;
    public float shootCooldown = 1.5f;
    private float currentShootCooldown = 0;
    private bool playerEverSeen = false;

    public GameObject boltPrefab;
    public float boltEnemyDistance = 0.4f, bulletLifeTime = 5f, bulletVelocity = 2f;

    void Start()
    {
        rayCast = transform.GetComponent<RayCastSimple>();
        stats = transform.GetComponent<EnemyStats>();
    }

    void FixedUpdate()
    {
        if (rayCast.seePlayer)
        {
            playerEverSeen = true;
            if (currentShootCooldown <= 0)
            {
                Vector3 direction = (rayCast.lastPlayerPosition - transform.position).normalized;
                GameObject bolt = Instantiate(boltPrefab, transform.position + direction * boltEnemyDistance, Quaternion.identity);
                //Vector3 direction = mouseVector + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i]), Mathf.Sin(Mathf.Deg2Rad * angles[i]));
                bolt.GetComponent<Rigidbody2D>().velocity = bulletVelocity * direction;
                bolt.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(bolt, bulletLifeTime);
                currentShootCooldown = shootCooldown;
            }

            if (Vector2.Distance(transform.position, rayCast.lastPlayerPosition) > maxPlayerDistance)
            {
                transform.position += (rayCast.lastPlayerPosition - transform.position).normalized * stats.movementSpeed * Time.fixedDeltaTime;
                //animator.SetBool("PlayerSeen", true);
            }
            else if (Vector2.Distance(transform.position, rayCast.lastPlayerPosition) < minPlayerDistance)
            {
                transform.position -= (rayCast.lastPlayerPosition - transform.position).normalized * stats.movementSpeed * Time.fixedDeltaTime;
                //animator.SetBool("PlayerSeen", false);
            }
        }
        else
        {
            if (playerEverSeen && Vector2.Distance(transform.position, rayCast.lastPlayerPosition) > 0.05)
            {
                transform.position += (rayCast.lastPlayerPosition - transform.position).normalized * stats.movementSpeed * Time.fixedDeltaTime;
            }
        }

        

        if (currentShootCooldown >= 0)
        {
            currentShootCooldown -= Time.fixedDeltaTime;
        }
    }
}
