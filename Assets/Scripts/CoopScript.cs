using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The primary handler for the coop scene functionality, cooperates with the Game Controller to make it run smoothly.
/// Only appears in the coop scene. In retrospect, it probably should have been called the Versus script. 
/// </summary>
public class CoopScript : MonoBehaviour
{
    //Basic variables used for calculations.
    public int turncount = 0;
    public int enemyCharges = 5;
    GameController controller = null;
    EnemyMissileSpawner missileSpawner = null;
    GameObject[] defenders;
    private double timer = 0.0f;
    private double playerOneScore = 0.0f;
    private double playerTwoScore = 0.0f;

    //TEXTGUI
    [SerializeField] private TextMeshProUGUI winnerTextField = null;
    [SerializeField] private TextMeshProUGUI enemyChargeText = null;
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private TextMeshProUGUI roundCountdown = null;
    [SerializeField] private TextMeshProUGUI player1scoretext = null;
    [SerializeField] private TextMeshProUGUI player1finalscoretext = null;
    [SerializeField] private TextMeshProUGUI player2finalscoretext = null;
    [SerializeField] private GameObject coopStartScreen = null;
    [SerializeField] private GameObject roundOverScreen = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private Button closeStartScreen = null;



    //Bool flags
    private bool addedEnemy = false;
    private bool addedPlayerMissile = false;
    public bool leftPress = false;
    public bool downPress = false;
    public bool rightPress = false;
    public bool coopGameOver = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Retrieves all the relevant gameobjects. 
        controller = FindObjectOfType<GameController>();
        missileSpawner = FindObjectOfType<EnemyMissileSpawner>();
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isRoundOver == false) //Checks if the round 
        {
            StartCoroutine(ReloadMissiles());
            StartCoroutine(ReloadEnemies());
            timerText.text = "Survived for: " + timer.ToString("#.##");
            timer += Time.deltaTime;
            if (turncount == 0)
            {
                playerOneScore = timer;
            }
            else
            {
                playerTwoScore = timer;
            }
            if (enemyCharges > 0) //Only reloads if enemy charges is larger than 0. 
            {
                FireEnemy();
                UpdateEnemytext();
            }
        }
    }
    public void LoadCoop() //Public function to call the coroutine from elsewhere. 
    {
        StartCoroutine(LoadIn());
    }


    public IEnumerator LoadIn() //Coroutine to wait for the load-in animation to play. 
    {
        yield return new WaitForSeconds(1.5f);
        InstructionScreen();
    }
    private IEnumerator ReloadMissiles()
    {
        //Coroutine to increase the missile count every two seconds. 
        while (controller.playermissilesLeft < 15 && addedPlayerMissile == false)
        {
            addedPlayerMissile = true;
            yield return new WaitForSeconds(2.0f);
            controller.playermissilesLeft++;
            
            controller.UpdateMissilesLeftText();
            addedPlayerMissile = false;
        }
        
        

    }
    private IEnumerator ReloadEnemies() //Code to determine when to reload enemies for the keyboard player. 
    {
        //Speed values for missiles and max count of enemies also increase over time. 
        while (enemyCharges < 5 && addedEnemy == false && timer < 10)
        {
            addedEnemy = true;
            yield return new WaitForSeconds(1.5f);
            enemyCharges++;

            UpdateEnemytext();
            addedEnemy = false;
        }
        while (enemyCharges < 8 && addedEnemy == false && (timer > 10 && timer < 30 ))
        {
            addedEnemy = true;
            yield return new WaitForSeconds(1.2f);
            enemyCharges++;
            controller.enemymissileSpeed = 3.7f;
            UpdateEnemytext();
            addedEnemy = false;
        }
        while (enemyCharges < 8 && addedEnemy == false && (timer < 60 && timer > 30))
        {
            addedEnemy = true;
            yield return new WaitForSeconds(0.7f);
            enemyCharges++;
            controller.enemymissileSpeed = 5f;
            UpdateEnemytext();
            addedEnemy = false;
        }
        while (enemyCharges < 10 && addedEnemy == false && timer > 60)
        {
            addedEnemy = true;
            yield return new WaitForSeconds(0.5f);
            enemyCharges++;
            controller.enemymissileSpeed = 6f;
            UpdateEnemytext();
            addedEnemy = false;
        }


    }
    private void InstructionScreen() //Function that displays the start screen on load in. 
    {
        Time.timeScale = 0.0f;
        coopStartScreen.SetActive(true);
        closeStartScreen.onClick.AddListener(CloseScreen);

    }
    private void CloseScreen() //Actions to happen when the start screen is closed. 
    {
        Time.timeScale = 1.0f;
        coopStartScreen.SetActive(false);
        controller.gameStart = true;
        timer = 0;
        timerText.gameObject.SetActive(true);
    }
    private void UpdateEnemytext() //Simple updater for the text screen. 
    {
        enemyChargeText.text = "Enemy Missiles Left: " + enemyCharges;
    }
    private void FireEnemy() 
    {
        //Logic for firing the enemies as the keyboard player.
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downPress = true;
            leftPress = false;
            rightPress = false;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();
            UpdateEnemytext();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            downPress = false;
            leftPress = true;
            rightPress = false;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();
            UpdateEnemytext();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            downPress = false;
            leftPress = false;
            rightPress = true;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();
            UpdateEnemytext();
        }
    }
    public void RoundOverCoop() //Similar to the LoadIn function, this allows for external usage of Coroutines. 
    {
        StartCoroutine(RoundOverScreen());
    }
    
    public IEnumerator RoundOverScreen()
    {
        //Handles what happens after the round is over. Includes showing a scoreboard and resetting values.
        yield return new WaitForSeconds(0.5f);

        roundOverScreen.SetActive(true);
        
        player1scoretext.text = "Player one survived for: " + playerOneScore.ToString("#.##");

        turncount++;
        timer = 0;
        timerText.text = "Survived for: " + timer.ToString("#.##") + " scs";
        controller.playermissilesLeft = 15;
        enemyCharges = 5;
        controller.enemymissileSpeed = 3f;

        //Resets the animation states of the objects back to alive, for the next round. 
        foreach (GameObject defender in defenders)
        {
            Animator reviveanim = defender.GetComponent<Animator>();
            reviveanim.SetTrigger("ReviveTrigger");
            reviveanim.ResetTrigger("DeathTrigger");

        }
        //Countdown before next round starts.
        roundCountdown.text = "5";
        yield return new WaitForSeconds(1f);
        roundCountdown.text = "4";
        yield return new WaitForSeconds(1f);
        roundCountdown.text = "3";
        yield return new WaitForSeconds(1f);
        roundCountdown.text = "2";
        yield return new WaitForSeconds(1f);
        roundCountdown.text = "1";
        yield return new WaitForSeconds(1f);
        
        roundOverScreen.SetActive(false);
        
        controller.isRoundOver = false;
    }
    public void CoopGameOver() //What the game does when the Coop Mode hits a gameover state. 
    {
        //Sets the gameover screen and calculates the scores of both players, as well as the winner. 
        gameOverScreen.SetActive(true);
        playerTwoScore = timer;
        player1finalscoretext.text = "Player One: " + playerOneScore.ToString("#.##") + " seconds";
        player2finalscoretext.text = "Player Two: " + playerTwoScore.ToString("#.##") + " seconds";
        if (playerOneScore > playerTwoScore)
        {
            winnerTextField.text = "The Winner is... Player One!";
        }
        else
        {
            winnerTextField.text = "The Winner is... Player Two!";
        }
    }
}
