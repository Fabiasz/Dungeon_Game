using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject crossHair;
    public GameObject boltPrefab;
    private float boltCooldown = 0;

    public float movementSpeed;


    void Update()
    {
        MoveCharacter();
        AimAndShoot();
    }



    private void MoveCharacter()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Normalize();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    private void AimAndShoot()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        crossHair.transform.position = new Vector2(aim.x, aim.y);

        Vector2 aimDirection = (aim - transform.position);
        aimDirection.Normalize();
        if (Input.GetButton("Fire1") && boltCooldown <= 0)
        {
            GameObject bolt = Instantiate(boltPrefab, transform.position, Quaternion.identity);
            bolt.GetComponent<Rigidbody2D>().velocity = 7.0f * aimDirection;
            bolt.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
            Destroy(bolt, 1.35f);
            boltCooldown = 0.2f;
        }
        boltCooldown -= Time.deltaTime;
    }
}
