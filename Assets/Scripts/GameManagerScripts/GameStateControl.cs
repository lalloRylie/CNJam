using UnityEngine;
using System.Collections;

public class GameStateControl : MonoBehaviour {

    public int gameState = 0;
    float gameTimer = 0f;
   // float engageBossAfterTime = 60f;
    public float engageBossAfterTime = 10f;
    public GameObject bossPrefab = null;

    CutScene_TransitionToBoss cutScene;

    GameObject bossGO = null;
    BossHealth bossHealthScript = null;

    //public bool isBossDead = false;

    // Use this for initialization
    void Start () {
        cutScene = GetComponent<CutScene_TransitionToBoss>();
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

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("WinScreen");
    }

    bool winScreenTriggered = false;

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
                    GameObject.Find("Player").GetComponent<Player_Attack>().playerState = 2;
                }
                break;
            //Boss instantiate state
            case 1:
                // turn on cutscene for boss here
                cutScene.StartCutScene();
                //GameObject.Instantiate(bossPrefab);
                //SetGameState(2);
                break;
            //Boss fight state
            case 2:
                break;
            //Lose state
            case 3:
                ForceEnemiesToIdle();
                //GetComponent<LoseScreen>().OpenLoseScreenUI();
                StartCoroutine(InitiateLoseScreen());
                break;
        }

        if(bossGO == null)
        {
            bossGO = GameObject.FindGameObjectWithTag("Boss");
        }

        if (bossGO == null) return;

        if(bossHealthScript == null)
        {
            bossHealthScript = bossGO.GetComponent<BossHealth>();
        }

        if (bossHealthScript == null) return;

        if (bossHealthScript.isBossDead && !winScreenTriggered)
        {
            StartCoroutine(GoToMenu());
            winScreenTriggered = true;
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
