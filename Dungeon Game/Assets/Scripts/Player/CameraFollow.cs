using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Transform crossHair;


    private void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        crossHair = GameObject.FindGameObjectWithTag("CrossHair").GetComponent<Transform>();
        Debug.Log("Player" + player.position);
        Debug.Log("crossHair" + crossHair.position);
    }

    void Update()
    {
       
        transform.position = new Vector3((player.position.x + crossHair.position.x) / 2, (player.position.y + crossHair.position.y) / 2,
            transform.position.z);
    }
}
