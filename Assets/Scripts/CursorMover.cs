using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script contains the core elements of the cursor movement and targeting, as it also determines which silo is used.
/// Used to set the cursor, determine how it moves and also what the target for the missiles are. 
/// </summary>
public class CursorMover : MonoBehaviour
{
    //Serialize Fields set the textures and gameobjects used for calculations. 
    [SerializeField] private Texture2D cursorTexture = null;
    
    [SerializeField] GameObject missilePrefab = null;
    [SerializeField] GameObject[] missileLauncherPrefabs = null;
    private Vector2 cursorHotspot;
    GameObject bestTarget = null;
    private GameController myGameController;
    private Animator deathCheck = null;
    private int totalDeath = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Sets initial values for cursor and game controller. 
        myGameController = GameObject.FindObjectOfType<GameController>();
        cursorHotspot = new Vector2(cursorTexture.height / 2f, cursorTexture.width / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myGameController.playermissilesLeft > 0 && myGameController.gameStart == true)
        {
            GameObject bestTarget = GetClosestSilo(missileLauncherPrefabs);
            
            //Finds the best silo (closest, still alive) and creates missiles from it that fly towards the target. 
            if (bestTarget != null && totalDeath < 3)
            {
                DeathCounter(); //Checks to ensure that there is still at least one alive silo. 
                if (totalDeath < 3)
                {
                    Object.Instantiate(missilePrefab, bestTarget.transform.position, Quaternion.identity);
                    myGameController.playermissilesLeft--;
                    myGameController.UpdateMissilesLeftText();
                }
            }
        }
    }

    
    private GameObject GetClosestSilo (GameObject[] missileLauncherPrefabs)
    {
        
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach(GameObject potentialTarget in missileLauncherPrefabs)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - new Vector3(currentPosition.x, currentPosition.y, 0);
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            deathCheck = potentialTarget.GetComponent<Animator>();
            if(dSqrToTarget < closestDistanceSqr && deathCheck.GetCurrentAnimatorStateInfo(0).IsName("siloalive"))
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
            
       }
       return bestTarget;
    }
    
    public int DeathCounter() //Value checking for all of the Silos dying, checked separately due to the unique behaviour of silos.
    {
        totalDeath = 0;
        foreach (GameObject missilelauncher in missileLauncherPrefabs)
        {
            
            deathCheck = missilelauncher.GetComponent<Animator>();
            if (!deathCheck.GetCurrentAnimatorStateInfo(0).IsName("siloalive"))
            {
                totalDeath++;
            }
        }return totalDeath;
    }
}
