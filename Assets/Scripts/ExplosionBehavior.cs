using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    [SerializeField] float radius = 2f; //Radius is the max value of the Lerp
    
    [SerializeField] GameObject explosionPrefab = null;



    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Dynamic list containing all the enemy missiles, required for finding the radius. 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length ; i++ ) 
        {
            float distance = Vector3.Distance(gameObject.transform.position, enemies[i].transform.position);
            if (distance < radius)
            {
                //This will add the points for destroying a missile as well as spawning another explosion. 
                FindObjectOfType<GameController>().AddMissileDestroyedPoints();
                Object.Instantiate(explosionPrefab, new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, enemies[i].transform.position.z), Quaternion.identity);
                Destroy(enemies[i]);
                
            }
        }
        
    }
  
}
