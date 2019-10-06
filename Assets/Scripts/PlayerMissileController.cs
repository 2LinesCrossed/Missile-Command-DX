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
