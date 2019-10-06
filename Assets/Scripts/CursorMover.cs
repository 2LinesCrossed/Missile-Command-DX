using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMover : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;
    
    [SerializeField] GameObject missilePrefab = null;
    [SerializeField] GameObject[] missileLauncherPrefabs = null;
    private Vector2 cursorHotspot;
    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.height / 2f, cursorTexture.width / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bestTarget = GetClosestSilo(missileLauncherPrefabs);
            //The missile spawning code. Change this later to rotate the missile properly and for it to only spawn missiles from the closest silo.

            
            Object.Instantiate(missilePrefab, bestTarget.transform.position,Quaternion.identity); 
        }
    }

    
    private GameObject GetClosestSilo (GameObject[] missileLauncherPrefabs)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach(GameObject potentialTarget in missileLauncherPrefabs)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - new Vector3(currentPosition.x, currentPosition.y, 0);
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
       }
       return bestTarget;
    }
    
    
}
