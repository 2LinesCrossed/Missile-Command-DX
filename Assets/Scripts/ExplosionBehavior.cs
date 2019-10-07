using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    [SerializeField] float radius = 2f;
    
    [SerializeField] GameObject explosionPrefab = null;



    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length ; i++ ) 
        {
            float distance = Vector3.Distance(gameObject.transform.position, enemies[i].transform.position);
            if (distance < radius)
            {
                //This will add the points for destroying a missile. 
                FindObjectOfType<GameController>().AddMissileDestroyedPoints();
                Object.Instantiate(explosionPrefab, new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, -2), Quaternion.identity);
                Destroy(enemies[i]);
                
            }
        }
        //Maybe you shouldn't do this? I think it would be better for the missile to handle the instantiation. 
        /*
        Vector3 positionA = transform.position;
        if (Vector3.Distance(positionA, enemy.transform.position) > radius) //This is currently an infinite loop that breaks the game
        {
            destroyEnemy();
        }
        */
    }
  
}
