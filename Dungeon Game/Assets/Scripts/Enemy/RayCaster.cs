using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCaster : MonoBehaviour
{
    public int rotSpeed = 200;
    public float distance = 20;
    [SerializeField]
    GameObject enemy;
    Enemy enemyscript;
    EnemyFollowing enemyFollowing;
    public float followTime = 5f;
    bool following = false;
    public Animator animator;
    private float timeBeetweenSeeingPlayer = 2f;
    Vector2 lastPlayerSeenPos;
    private bool isOnPlace = false;

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
        {
            enemyFollowing.FollowPlayer(new Vector2(0, 0), false);
            transform.Rotate(Vector3.forward * rotSpeed);

        }
        else {
            enemyFollowing.FollowPlayer(lastPlayerSeenPos, true);
        }


        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.tag == "Player")
            {
                
                Debug.Log("Collision");

                following = true;
                
                enemyscript.Shooting();
                
                timeBeetweenSeeingPlayer = 2f;
                lastPlayerSeenPos = hitInfo.collider.transform.position;
            }
            else
            {
                timeBeetweenSeeingPlayer -= Time.deltaTime;
                if(timeBeetweenSeeingPlayer <= 0)
                {
                    following = false;
                    
                    /*if (!isOnPlace)
                    {
                        enemyFollowing.FollowPlayer(lastPlayerSeenPos, true);
                    }*/

                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.yellow);

        }
    }
}
