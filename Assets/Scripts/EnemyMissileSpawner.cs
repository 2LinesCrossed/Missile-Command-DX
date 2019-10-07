using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private float yPadding = 0.5f; //Adjust how far up the missile spawns
    private float minX, maxX;
    public int missileToSpawn = 10;
    public float missileDelay = 0.5f;
    float yValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the minimum and the maximum spawn values to be at the top of the screen, using the camera.
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public IEnumerator SpawnMissiles()
    {
        while (missileToSpawn > 0)
        {
            float randomX = Random.Range(minX, maxX);
            float yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            Instantiate(enemyPrefab, new Vector3(randomX, yValue + yPadding, 0), Quaternion.identity);
            --missileToSpawn;
            yield return new WaitForSeconds(missileDelay); //Waits before firing more missiles 
        }
    }
    public void StartRound()
    {
        StartCoroutine(SpawnMissiles());
    }
}
