using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSimple : MonoBehaviour
{
    private RaycastHit2D hitInfo;
    private GameObject player;
    [HideInInspector]
    public Vector2 lastPlayerPosition;
    private Vector2 direction;
    [HideInInspector]
    public bool seePlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPosition.x = transform.position.x;
        lastPlayerPosition.y = transform.position.y;
        Physics2D.queriesStartInColliders = false;
        //LayerMask layerMask = LayerMask
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        hitInfo = Physics2D.Raycast(transform.position, direction);
        //Physics2D.Raycast()
        if (hitInfo.collider != null)
        {
            
            if (hitInfo.collider.tag == "Player")
            {
                Debug.Log("Collision");
                lastPlayerPosition.x = player.transform.position.x;
                lastPlayerPosition.y = player.transform.position.y;
                Debug.Log("lastPlayerPosition: " + lastPlayerPosition);
                
                //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                Debug.DrawLine(transform.position, player.transform.position, Color.green);
                seePlayer = true;
            }
            else
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.yellow);
                seePlayer = false;

            }
        }
        else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);

        }

    }
}

