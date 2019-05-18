using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float stopDist = 5f;
    [SerializeField] float retreatDist = 2f;
    private Transform player;
    Animator animator;

    void Start()
    {
        Debug.Log("Hellow World");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = transform.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // FollowPlayer();
    }

    public void FollowPlayer(Vector2 lastPos , bool activate_last = false)
    {
        if (!activate_last)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDist)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                animator.SetBool("PlayerSeen", true);
            }
            else if (Vector2.Distance(transform.position, player.position) < stopDist && Vector2.Distance(transform.position, player.position) > retreatDist)
            {
                transform.position = this.transform.position;
                animator.SetBool("PlayerSeen", false);
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                animator.SetBool("PlayerSeen", true);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);
            animator.SetBool("PlayerSeen", true);
            if(transform.position.x == lastPos.x && transform.position.y == lastPos.y)
            {
                animator.SetBool("PlayerSeen", false);
            }
        }
    }
}
