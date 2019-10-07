using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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


    //Calculations
    //Animator death = null;
    private bool isRoundOver = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();
        UpdateScoreText();
        UpdateLevelText();
        UpdateMissilesLeftText();
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyMissilesLeftInRound);
        if (enemyMissilesLeftInRound <= 0 && isRoundOver == false)
        {
            isRoundOver = true;
            StartCoroutine(EndOfRound());
        }
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
    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(0.5f);
         
        endOfRoundPanel.SetActive(true);
        int leftOverMissileBonus = playermissilesLeft * missileEndOfRoundpoints;
        int citycount = 0;
        CityController[] cities = GameObject.FindObjectsOfType<CityController>();
        foreach (CityController city in cities)

            if (city.CityDead() == false)
             {
            citycount++;
             }

        int leftOverCityBonus = citycount * cityEndOfRoundpoints;
        int totalBonus = leftOverCityBonus + leftOverMissileBonus;
        leftOverMissileBonusText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityBonusText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Left over missile bonus: " + totalBonus;
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
}
