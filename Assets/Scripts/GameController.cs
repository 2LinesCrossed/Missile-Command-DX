using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public int missilesLeft = 30;


    [SerializeField] private TextMeshProUGUI myScoreText = null;
    [SerializeField] private TextMeshProUGUI myLevelText = null;
    [SerializeField] private TextMeshProUGUI myMissilesLeftText = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        UpdateLevelText();
        UpdateMissilesLeftText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMissilesLeftText()
    {
        myMissilesLeftText.text = "Missiles Left: " + missilesLeft;
    }
    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }
    public void UpdateLevelText()
    {
        myLevelText.text = "Level: " + level;
    }


}
