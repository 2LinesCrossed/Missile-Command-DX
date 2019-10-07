using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public int playermissilesLeft = 30;
    private int enemyMissilesThisRound = 20;
    private int enemyMissilesLeftInRound = 20;
    EnemyMissileSpawner enemyMissileSpawner = null;

    //Score Values
    private int missileDestroyedPoints = 25;
    private int cityLossPenalty = 100;
    private int launcherLossPenalty = 200;

    [SerializeField] private TextMeshProUGUI myScoreText = null;
    [SerializeField] private TextMeshProUGUI myLevelText = null;
    [SerializeField] private TextMeshProUGUI myMissilesLeftText = null;

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

    public void missileDestroyedCity()
    {
        score -= cityLossPenalty;
        enemyMissilesLeftInRound--;
        UpdateScoreText();
    }

    public void missileDestroyedSilo()
    {
        score -= launcherLossPenalty;
        enemyMissilesLeftInRound--;
        UpdateScoreText();
    }

    private void StartRound()
    {
        enemyMissileSpawner.missileToSpawn = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;
        enemyMissileSpawner.StartRound();
    }
}
