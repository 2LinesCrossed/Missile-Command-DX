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
    private int sceneIndex;
    //dummy comment
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

        SceneSwap();
        
        

    }
    public void SceneSwap()
    {
        if (Input.GetKeyDown(KeyCode.S) && sceneIndex != 1)
        {
            sceneName = "MainGame";
            StartCoroutine(LoadScene());
        }
        if (Input.GetKeyDown(KeyCode.Q) && sceneIndex != 0)
        {
            sceneName = "Title Screen";
            StartCoroutine(LoadScene());
        }
    }
    public void SceneReset()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        
        sceneSition.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        sceneSition.ResetTrigger("end");
        
        
    }
    
}
