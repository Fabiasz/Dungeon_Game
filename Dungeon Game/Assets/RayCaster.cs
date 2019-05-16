using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCaster : MonoBehaviour
{
    public int rotSpeed = 200;
    public float distance = 5;
    [SerializeField]
    GameObject enemy;
    Enemy enemyscript;
    EnemyFollowing enemyFollowing;
    public float followTime = 5f;
    bool following = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
         enemyscript = transform.parent.gameObject.GetComponent<Enemy>();
         enemyFollowing = transform.parent.gameObject.GetComponent<EnemyFollowing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
            enemyFollowing.FollowPlayer();
        transform.Rotate(Vector3.forward * rotSpeed   );

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.tag == "Player")
            {
                Debug.Log("Collision");

                following = true;
                   
                

                enemyscript.Shooting();
            }
            else
            {
                ;
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.yellow);

        }
    }
}
