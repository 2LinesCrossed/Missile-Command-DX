using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMover : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherPrefab;
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
            //The missile spawning code. Change this later to rotate the missile properly and for it to only spawn missiles from the closest silo.
            Object.Instantiate(missilePrefab, missileLauncherPrefab.transform.position,Quaternion.identity); 
        }
    }
}
