using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public float GetSpeed()
    {
        return moveSpeed;
    }
    public List<Transform> GetwayPoints()
    {
        var wayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            wayPoints.Add(child.transform);
           // Debug.Log(child.ToString());
        }
        return wayPoints;
    } 
    

}
