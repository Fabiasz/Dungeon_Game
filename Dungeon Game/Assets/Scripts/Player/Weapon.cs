using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public int bulletsPerShot = 1;
    [Range(0, 360)]
    public float widthAngle = 0;
    private float[] angles;
    [Range(0, 360)]
    public float spreadAngle = 0;
    public float bulletWeaponDistance = 0.1f;
    public float bulletVelocity = 5;
    public float timeBetweenBullets = 0.2f;
    public float bulletLifeTime = 1.5f;
    public float manaPerShot = 5;
    private PlayerStats playerStats;

    private void Start()
    {

        weaponSprite = gameObject.GetComponent<Transform>();
        player = transform.root.GetComponentInChildren<Transform>();
        playerStats = player.GetComponent<PlayerStats>();

        angles = new float[bulletsPerShot];
        if (bulletsPerShot == 1)
        {
            angles[0] = 0;
        }
        else
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                angles[i] = -widthAngle / 2 + i * widthAngle / (bulletsPerShot - 1);
            }
        }
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
        if (firePressed && boltCooldown <= 0 && playerStats.mana >= manaPerShot)
        {
            float spread = (Random.value - 0.5f);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                GameObject bolt = Instantiate(boltPrefab, transform.position + mouseVector * bulletWeaponDistance, Quaternion.identity);
                Vector3 direction = Quaternion.Euler(0, 0, angles[i] + spread * spreadAngle) * mouseVector;
                bolt.GetComponent<Rigidbody2D>().velocity = bulletVelocity * direction;
                bolt.transform.Rotate(0.0f, 0.0f, angles[i] + Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg);
                Destroy(bolt, bulletLifeTime);
            }
            boltCooldown = timeBetweenBullets;
            playerStats.mana -= manaPerShot;
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
