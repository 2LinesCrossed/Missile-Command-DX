using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player Missile Spawner. Actually uses values from Cursormover. 
/// This one instantiates the missiles and rotates them properly. 
/// </summary>
public class PlayerMissileController : MonoBehaviour
{
    private Vector2 target = new Vector2();
    [SerializeField] private float speed = 8;
    [SerializeField] private GameObject explosionPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the location of the left-click for the missile's target. 
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (target - (Vector2)transform.position);



        //Trigonometry-based math rotation of the missile to face the direction it's going. Because of the way I had
        //set up my Unity scene, it seemed to be frustratingly complicated to actually make it happen. 
        //Probably something I'm going to have to revise during the holidays. 
        float angle = 0;
        if (direction.x > 0)
            angle = 90 - Mathf.Acos(direction.x / direction.magnitude) / Mathf.PI * 180.0f;
        else
            angle = (Mathf.Acos(-direction.x / direction.magnitude) / Mathf.PI * 180.0f - 90);

        transform.rotation = Quaternion.Euler(0, 0, -angle);


        
    }

    // Update is called once per frame
    void Update()
    {
        //Moves missile towards target until it hits it. 
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (transform.position == (Vector3)target)
        {
            Object.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
