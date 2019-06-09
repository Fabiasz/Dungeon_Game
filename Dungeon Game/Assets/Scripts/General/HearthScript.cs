using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthScript : MonoBehaviour
{
    public int healtRegen = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().ChangeHealth(-healtRegen);
            Destroy(this.gameObject);
        }
    }
}
