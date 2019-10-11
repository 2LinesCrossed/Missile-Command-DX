using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoopScript : MonoBehaviour
{
    public int turncount = 0;
    public int enemyCharges = 5;
    GameController controller = null;
    EnemyMissileSpawner missileSpawner = null;
    private double timer = 0.0f;
    [SerializeField] private TextMeshProUGUI totalBonusText = null;
    [SerializeField] private TextMeshProUGUI CountdownText = null;
    [SerializeField] private GameObject coopStartScreen = null;
    [SerializeField] private GameObject roundOverScreen = null;
    [SerializeField] private Button closeStartScreen = null;


    private bool addedPlayerMissile = false;
    public bool leftPress = false;
    public bool downPress = false;
    public bool rightPress = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        missileSpawner = FindObjectOfType<EnemyMissileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
 
        StartCoroutine(ReloadMissiles());
        CountdownText.text = "Survived for: " + timer.ToString("#.##");
        timer += Time.deltaTime;
        FireEnemy();
    }
    public void LoadCoop()
    {
        StartCoroutine(LoadIn());
    }


    public IEnumerator LoadIn()
    {
        yield return new WaitForSeconds(1.5f);
        InstructionScreen();
    }
    private IEnumerator ReloadMissiles()
    {
        
        while (controller.playermissilesLeft < 15 && addedPlayerMissile == false)
        {
            addedPlayerMissile = true;
            yield return new WaitForSeconds(2.0f);
            controller.playermissilesLeft++;
            Debug.Log("Added a missile");
            controller.UpdateMissilesLeftText();
            addedPlayerMissile = false;
        }
        
        

    }
    private void InstructionScreen()
    {
        Time.timeScale = 0.0f;
        coopStartScreen.SetActive(true);
        closeStartScreen.onClick.AddListener(CloseScreen);

    }
    private void CloseScreen()
    {
        Time.timeScale = 1.0f;
        coopStartScreen.SetActive(false);
        controller.gameStart = true;
        timer = 0;
        CountdownText.gameObject.SetActive(true);
    }
    private void FireEnemy()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downPress = true;
            leftPress = false;
            rightPress = false;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            downPress = false;
            leftPress = true;
            rightPress = false;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            downPress = false;
            leftPress = false;
            rightPress = true;
            missileSpawner.missileToSpawn++;
            enemyCharges--;
            missileSpawner.StartRound();

        }
    }
}
