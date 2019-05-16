using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float distanceFromPlayer = 0.2f;
    private Vector3 mousePos, mouseVector;
    private Transform weaponSprite;
    private Transform player;
    private GameObject playerObject;

    private float boltCooldown = 0;
    private bool firePressed = false;
    public GameObject boltPrefab;
    public float bulletWeaponDistance = 0.1f;
    public float bulletVelocity = 5;
    public float timeBetweenBullets = 0.2f;
    public float bulletLifeTime = 1.5f;

    private void Start()
    {

        weaponSprite = gameObject.GetComponent<Transform>();
        player = transform.root.GetComponentInChildren<Transform>();
        //player = gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        GetMouseInput();
        AimDirection();
    }

    private void FixedUpdate()
    {
        Fire();
    }

    private void Fire()
    {
        if (firePressed && boltCooldown <= 0)
        {
            GameObject bolt = Instantiate(boltPrefab, transform.position + mouseVector * bulletWeaponDistance, Quaternion.identity);
            bolt.GetComponent<Rigidbody2D>().velocity = bulletVelocity * mouseVector;
            bolt.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg);
            Destroy(bolt, bulletLifeTime);
            boltCooldown = timeBetweenBullets;
        }
        boltCooldown -= Time.fixedDeltaTime;
    }

    private void GetMouseInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //position of cursor in world
        mousePos.z = player.position.z; //keep the z position consistant, since we're in 2d
        mouseVector = (mousePos - player.position).normalized; //normalized vector from player pointing to cursor
        //mouseLeft = Input.GetMouseButton(0); //check left mouse button

        firePressed = Input.GetButton("Fire1");
    }

    private void AimDirection()
    {
        float gunAngle = -1 * Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg; //find angle in degrees from player to cursor
        weaponSprite.rotation = Quaternion.AngleAxis(gunAngle + 90, Vector3.back); //rotate gun sprite around that angle
        weaponSprite.position = player.position + distanceFromPlayer * mouseVector;
    }
}
