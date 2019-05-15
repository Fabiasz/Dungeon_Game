using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject crossHair;
    //public GameObject boltPrefab;
    public GameObject weapon1Prefab, weapon2Prefab;
    private GameObject weapon1, weapon2;
    //private float boltCooldown = 0;
    private Rigidbody2D rb;
    private PlayerStats stats;
    private Vector3 currentMovement;

    //public float movementSpeed;
    //public float boltVelocity = 7.0f;

    private void Awake()
    {
        weapon1 = Instantiate(weapon1Prefab, transform);
        //weapon1.transform.SetParent(transform);
        weapon1.SetActive(true);
        weapon2 = Instantiate(weapon2Prefab, transform);
        //weapon2.transform.SetParent(transform);
        weapon2.SetActive(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        
    }

    private void Update()
    {
        currentMovement = MoveCharacter();
        AimAndShoot();
        ChangeWeapon();
    }

    private void FixedUpdate()
    {
        PhysicsMoveCharacter();
    }

    private void PhysicsMoveCharacter()
    {
        transform.position += currentMovement;
    }

    private Vector3 MoveCharacter()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //movement.Normalize();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        //GetComponent<Rigidbody2D>().velocity = movement * movementSpeed;
        return movement * stats.movementSpeed * Time.deltaTime;
    }

    private void AimAndShoot()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        crossHair.transform.position = new Vector2(aim.x, aim.y);
    }

    private void ChangeWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (weapon1.activeSelf == true)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
            else
            {
                weapon2.SetActive(false);
                weapon1.SetActive(true);
            }
        }
    }
}
