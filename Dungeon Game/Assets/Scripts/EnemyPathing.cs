using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> wayPoints;
    [SerializeField]
    WaveConfig waveConfig;
    int waypointIndex = 0;

    private void Start()
    {
        wayPoints = waveConfig.GetwayPoints();
        transform.position = wayPoints[waypointIndex].transform.position;
        

    }
    private void Update()
    {
        makePathing();
       
       // Debug.Log(wayPoints.Count);
    }
    public void makePathing()
    {
        if (waypointIndex < wayPoints.Count - 1)
        {
            var targetPos = wayPoints[waypointIndex].transform.position;
            Debug.Log(targetPos.ToString());
            Debug.Log("thid" + transform.position.ToString());

            var moveFrame = waveConfig.GetSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveFrame);
            if ((transform.position.x == targetPos.x) && (targetPos.y == transform.position.y))
            {
               
                waypointIndex++;
            } 
        }
    }

}
