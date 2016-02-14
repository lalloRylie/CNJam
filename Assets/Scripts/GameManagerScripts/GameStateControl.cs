using UnityEngine;
using System.Collections;

public class GameStateControl : MonoBehaviour {

    public int gameState = 0;
    float gameTimer = 0f;
    float bossTimer = 0f;
    float engageBossAfterTime = 60f;
    float bossGroundPhaseAfterTime = 10f;
    float bossAirPhaseAfterTime = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState) {
            //Enemy battle state
            case 0:
                break;
            //Boss battle state
            case 1:
                break;
            //Win state
            case 2:
                break;
            //Lose state
            case 3:
                ForceEnemiesToIdle();
                GetComponent<LoseScreen>().OpenLoseScreenUI();
                break;
        }
	}

    public void SetGameState(int state)
    {
        gameState = state;
    }

    void ForceEnemiesToIdle()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // tell each enemy to go to idle state
            enemy.GetComponent<EnemyBehavior>().enemyState = 0;
        }
    }
}
