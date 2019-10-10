using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This manages some sound effects, such as low missiles/no missiles, the round over music and the game over music. 

/// </summary>
public class SoundController : MonoBehaviour
{
    GameController controller = null;
    AudioSource sfx = null;
    [SerializeField] AudioClip roundStart = null;
    [SerializeField] AudioClip lowMissileWarning = null;
    [SerializeField] AudioClip noMissiles = null;
    [SerializeField] AudioClip gameOver = null;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        sfx = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRoundSFX()
    {
        sfx.clip = roundStart;
        sfx.Play();

    }
    public void LowMissileSFX()
    {
        sfx.clip = lowMissileWarning;
        sfx.Play();
    }
    public void NoMissilesSFX()
    {
        sfx.clip = noMissiles;
        sfx.Play();
    }
    public void GameOverSFX()
    {
        sfx.clip = gameOver;
        sfx.Play();
    }
}
