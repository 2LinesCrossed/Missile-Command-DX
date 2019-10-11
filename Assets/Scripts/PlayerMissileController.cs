using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    private Vector2 target = new Vector2();
    [SerializeField] private float speed = 8;
    [SerializeField] private GameObject explosionPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (target - (Vector2)transform.position);

        float angle = 0;
        if (direction.x > 0)
            angle = 90 - Mathf.Acos(direction.x / direction.magnitude) / Mathf.PI * 180.0f;
        else
            angle = (Mathf.Acos(-direction.x / direction.magnitude) / Mathf.PI * 180.0f - 90);

        transform.rotation = Quaternion.Euler(0, 0, -angle);


        /*
        var travelVec = ((Vector3)target - transform.position);
        var radAngle = Mathf.Acos(travelVec.x / travelVec.magnitude);
        transform.Rotate(0, 0, radAngle); 
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (transform.position == (Vector3)target)
        {
            Object.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
