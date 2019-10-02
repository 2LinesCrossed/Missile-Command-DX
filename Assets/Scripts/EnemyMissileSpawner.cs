using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private float yPadding = 0.5f; //Adjust how far up the missile spawns
    private float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the minimum and the maximum spawn values to be at the top of the screen, using the camera.
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        float randomX = Random.Range(minX, maxX);
        float yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        Instantiate(enemyPrefab, new Vector3(randomX, yValue + yPadding, 0), Quaternion.identity); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
