using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{

    public int score = 0;

    int displayedScore = 0;

    public Text scoreText = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        displayedScore = (int)Mathf.Lerp(displayedScore, score, Time.deltaTime * 2f);
        
        scoreText.text = displayedScore.ToString();
    }
}
