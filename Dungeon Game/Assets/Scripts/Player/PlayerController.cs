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
    public GameObject newWeapon;
    //private float boltCooldown = 0;
    private Rigidbody2D rb;
    private PlayerStats stats;
    private Vector3 currentMovement;
    private int activeWeapon = 1;

    //public float movementSpeed;
    //public float boltVelocity = 7.0f;

    private void Awake()
    {
        weapon1 = Instantiate(weapon1Prefab, transform);
        weapon1.SetActive(true);
        weapon1.GetComponent<Weapon>().enabled = true;
        weapon1.GetComponent<CircleCollider2D>().enabled = false;
        weapon2 = Instantiate(weapon2Prefab, transform);
        weapon2.SetActive(false);
        weapon1.GetComponent<Weapon>().enabled = true;
        weapon1.GetComponent<CircleCollider2D>().enabled = false;
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
        PickupWeapon();
    }

    private void PickupWeapon()
    {
        if (Input.GetButtonDown("Interact") && newWeapon)
        {
            if (activeWeapon == 1)
            {
                weapon1Prefab = newWeapon;
                Destroy(newWeapon);
                newWeapon = Instantiate(weapon1, transform.position, Quaternion.identity);
                newWeapon.GetComponent<Weapon>().enabled = false;
                newWeapon.GetComponent<CircleCollider2D>().enabled = true;
                Destroy(weapon1);
                weapon1 = Instantiate(weapon1Prefab, transform);
                weapon1.GetComponent<Weapon>().enabled = true;
                weapon1.GetComponent<CircleCollider2D>().enabled = false;
            } else
            {
                weapon2Prefab = newWeapon;
                Destroy(newWeapon);
                newWeapon = Instantiate(weapon2, transform.position, Quaternion.identity);
                newWeapon.GetComponent<Weapon>().enabled = false;
                newWeapon.GetComponent<CircleCollider2D>().enabled = true;
                Destroy(weapon2);
                weapon2 = Instantiate(weapon2Prefab, transform);
                weapon2.GetComponent<Weapon>().enabled = true;
                weapon2.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
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
                activeWeapon = 2;
            }
            else
            {
                weapon2.SetActive(false);
                weapon1.SetActive(true);
                activeWeapon = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            newWeapon = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if(collision.gameObject == newWeapon)
            {
                newWeapon = null;
            }
        }
    }
}
