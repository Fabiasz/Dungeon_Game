using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform crossHair;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
       
        transform.position = new Vector3((player.position.x + crossHair.position.x) / 2, (player.position.y + crossHair.position.y) / 2,
            transform.position.z);
    }
}
