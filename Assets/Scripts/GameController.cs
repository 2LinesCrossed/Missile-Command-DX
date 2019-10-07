using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public float enemymissileSpeed = 3f;
    [SerializeField] private float enemyMissileSpeedMultiplier = 1.1f;
    public int playermissilesLeft = 30;
    private int enemyMissilesThisRound = 10;
    private int enemyMissilesLeftInRound = 10;
    EnemyMissileSpawner enemyMissileSpawner = null;
    [SerializeField] GameObject sceneloader = null;

    //Score Values
    private int missileDestroyedPoints = 25;
    //private int cityLossPenalty = 100;
    //private int launcherLossPenalty = 200;
    [SerializeField] private int missileEndOfRoundpoints = 50;
    [SerializeField] private int cityEndOfRoundpoints = 100;

    //TextGUI
    [SerializeField] private TextMeshProUGUI myScoreText = null;
    [SerializeField] private TextMeshProUGUI myLevelText = null;
    [SerializeField] private TextMeshProUGUI myMissilesLeftText = null;

    [SerializeField] private TextMeshProUGUI leftOverMissileBonusText = null;
    [SerializeField] private TextMeshProUGUI leftOverCityBonusText = null;
    [SerializeField] private TextMeshProUGUI totalBonusText = null;
    [SerializeField] private TextMeshProUGUI CountdownText = null;
    [SerializeField] private GameObject endOfRoundPanel = null;
    [SerializeField] private GameObject startGamePanel = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private TextMeshProUGUI FinalScoreText = null;
    [SerializeField] private Button closeScreen = null;


    //Calculations
    //Animator death = null;
    private bool isRoundOver = false;
    private bool gameOverState = false;
    int citycount = 6;
    [SerializeField] GameObject maincam = null;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();
        UpdateScoreText();
        UpdateLevelText();
        UpdateMissilesLeftText();
        //StartCoroutine(LoadIn()); Figure out how to make the program wait for 1.5 seconds before showing the instruction screen. 
        //InstructionScreen();
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyMissilesLeftInRound);
        if (enemyMissilesLeftInRound <= 0 && isRoundOver == false && gameOverState == false)
        {
            isRoundOver = true;
            StartCoroutine(EndOfRound());
        }
        
        GameOver();
    }
    private void InstructionScreen()
    {
        startGamePanel.SetActive(true);
        closeScreen.onClick.AddListener(CloseScreen);
        Time.timeScale = 0.0f;
    }
    private void CloseScreen()
    {
        Time.timeScale = 1.0f;
        startGamePanel.SetActive(false);
    }
    public void UpdateMissilesLeftText()
    {
        myMissilesLeftText.text = "Missiles Left: " + playermissilesLeft;
    }
    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }
    public void UpdateLevelText()
    {
        myLevelText.text = "Level: " + level;
    }

    public void AddMissileDestroyedPoints()
    {
        score += missileDestroyedPoints;
        enemyMissilesLeftInRound--;
        UpdateScoreText();
    }

    public void MissileDestroyedCity()
    {
        //score -= cityLossPenalty;
        enemyMissilesLeftInRound--;
        UpdateScoreText();
    }

    public void MissileDestroyedSilo()
    {
        //score -= launcherLossPenalty;
        enemyMissilesLeftInRound--;
        UpdateScoreText();
    }
    public void MissileHitDead()
    {
        enemyMissilesLeftInRound--;
    }

    private void StartRound()
    {
        enemyMissileSpawner.missileToSpawn = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;
        enemyMissileSpawner.StartRound();
    }
    private void CitySearch()
    {
        citycount = 6;
        CityController[] cities = GameObject.FindObjectsOfType<CityController>();
        foreach (CityController city in cities)
            
            if (city.CityDead() == true)
            {
                citycount--;
            }
            //Debug.Log(citycount);
    }

    private void GameOver()
    {
        int siloDeath = maincam.GetComponent<CursorMover>().DeathCounter();
        CitySearch();
        if ((citycount == 0 || siloDeath >= 3) )
        {
            //This currently seems to break specifically on silo-based game overs. But why?
            gameOverScreen.SetActive(true);
            gameOverState = true;
            FinalScoreText.text = "Final Score: " + score;
            if (Time.timeScale == 1.0f && isPaused == false)
            {
                Time.timeScale = 0.0f;
                isPaused = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && gameOverState == true)
                {
                    
                    Time.timeScale = 1.0f;
                    gameOverScreen.SetActive(false);
                    gameOverState = false;
                    ResetGame();
                }
            
        }
    }
    private void ResetGame()
    {
        /*score = 0;
        level = 1;
        playermissilesLeft = 30;
        enemymissileSpeed = 3f;
        enemyMissilesThisRound = 10;
        enemyMissilesLeftInRound = 10;
       */
        sceneloader.GetComponent<Scenetransition>().sceneName = "MainGame";
        sceneloader.GetComponent<Scenetransition>().SceneReset();
    }
    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(0.5f);
         
        endOfRoundPanel.SetActive(true);
        int leftOverMissileBonus = playermissilesLeft * missileEndOfRoundpoints;

        CitySearch();

        int leftOverCityBonus = citycount * cityEndOfRoundpoints;
        int totalBonus = leftOverCityBonus + leftOverMissileBonus;
        leftOverMissileBonusText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityBonusText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Total bonus: " + totalBonus;
        score += totalBonus;
        UpdateScoreText();

        //Updating new round values
        CountdownText.text = "5";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "4";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "3";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "2";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "1";
        yield return new WaitForSeconds(1f);

        endOfRoundPanel.SetActive(false);
        playermissilesLeft = 30;
        level++;
        enemymissileSpeed *= enemyMissileSpeedMultiplier;
        isRoundOver = false;
        StartRound();
        UpdateLevelText();
        UpdateMissilesLeftText();
    }
    public IEnumerator LoadIn()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
