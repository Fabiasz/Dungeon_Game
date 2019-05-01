using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;// Paddle width of 2
    [SerializeField] float maxX = 15f ;
    void Start()
    {
        
    }
    void Update()
    {
        //   Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits);  // 16 is width of a Screen in unity units
        
        Vector2 paddlePos = new Vector2(transform.position.x , transform.position.y);
        paddlePos.x = Mathf.Clamp(GetX(),minX,maxX); // returns positiion with limits
        transform.position = paddlePos;



    }
    public float GetX()
    {
        bool isAuto = FindObjectOfType<GameSession>().AutoPlay();
        if (isAuto)
        {
            return FindObjectOfType<Ball>().transform.position.x;
        }
        else
        {
            return (Input.mousePosition.x / Screen.width) * screenWidthInUnits;
        }
    }
}
