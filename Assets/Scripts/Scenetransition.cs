using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// This is meant to make the game change scenes - from starting on the main screen to the singleplayer or multiplayer
/// game mode. This happens when the player presses a corresponding button. 
/// </summary>
public class Scenetransition : MonoBehaviour
{
    public Animator sceneSition;
    public string sceneName;
    //dummy comment
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            sceneName = "MainGame";
            StartCoroutine(LoadScene());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sceneName = "Title Screen";
            StartCoroutine(LoadScene());
        }
        
        
        

    }
    //Note to self: Implement a swappable value for the scene name, so that it can work with both the scenes present. 
    IEnumerator LoadScene()
    {
        
        sceneSition.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        sceneSition.ResetTrigger("end");

    }
    
}
