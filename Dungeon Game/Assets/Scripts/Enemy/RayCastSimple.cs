using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSimple : MonoBehaviour
{
    private RaycastHit2D hitInfo;
    private GameObject player;
    [HideInInspector]
    public Vector3 lastPlayerPosition = new Vector3(0,0,0);
    private Vector2 direction;
    [HideInInspector]
    public bool seePlayer = false;
    private LayerMask layerMask;
    private ContactFilter2D contactFilter;

    private void Awake()
    {
        
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPosition.x = transform.position.x;
        lastPlayerPosition.y = transform.position.y;
        Physics2D.queriesStartInColliders = false;
        
        layerMask = LayerMask.GetMask("Terrain", "Player");
        Debug.Log("layerMask" + layerMask.ToString());
    }

    
    void Update()
    {
        direction = player.transform.position - transform.position;
        hitInfo = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);
        if (hitInfo.collider != null)
        {
            
            if (hitInfo.collider.tag == "Player")
            {
                //Debug.Log("Collision");
                lastPlayerPosition.x = player.transform.position.x;
                lastPlayerPosition.y = player.transform.position.y;
                //Debug.Log("lastPlayerPosition: " + lastPlayerPosition);
                
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

