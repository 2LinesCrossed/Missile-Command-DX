using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Behaviour script for the explosions spawned by the player and the enemies when they explode.
/// Normally would use a box collider/rigidbody, but this is a workaround. 
/// </summary>
public class ExplosionBehavior : MonoBehaviour
{
   
    
    [SerializeField] GameObject explosionPrefab = null;
    Transform explosion = null;


    
    // Start is called before the first frame update
    void Start()
    {
        explosion = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Dynamic list containing all the enemy missiles, required for finding the radius. 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length ; i++ ) 
        {
            float distance = Vector3.Distance(gameObject.transform.position, enemies[i].transform.position); //Checks how close enemies are to the explosion.
            double radius = explosion.localScale.x; //gets the size of the explosion animation to dynamically scale it, no lerps required!
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
