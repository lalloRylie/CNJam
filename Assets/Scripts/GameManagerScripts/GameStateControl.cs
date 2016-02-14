using UnityEngine;
using System.Collections;

public class GameStateControl : MonoBehaviour {

    public int gameState = 0;
    float gameTimer = 0f;
    float engageBossAfterTime = 30f;
    public GameObject bossPrefab = null;

	// Use this for initialization
	void Start () {
	
	}

    IEnumerator InitiateLoseScreen()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("LoseScreen");
    }

    void DeleteEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (gameState) {
            //Enemy battle state
            case 0:
                gameTimer += 1f * Time.deltaTime;
                if (gameTimer > engageBossAfterTime)
                {
                    SetGameState(1);
                    DeleteEnemies();
                    GameObject.Instantiate(bossPrefab);
                }
                    
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
                //GetComponent<LoseScreen>().OpenLoseScreenUI();
                StartCoroutine(InitiateLoseScreen());
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
