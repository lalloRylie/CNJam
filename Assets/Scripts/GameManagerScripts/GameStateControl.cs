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
    [HideInInspector]
    public bool bossCutsceneStarted = false;

    public bool bossWillBeSpawned = false;

    GameObject playerGO = null;

    public GameObject attackIcon = null;
    public GameObject staticBeamIcon = null;

    // Use this for initialization
    void Start () {
        cutScene = GetComponent<CutScene_TransitionToBoss>();
        AudioManager.instance.PlaySong(AudioManager.instance.gameNeutralMusic);

        DataCore.lastGameModePlayedSceneName = Application.loadedLevelName;
        
        if(Application.loadedLevelName == "EndlessGameMode") {
            bossWillBeSpawned = false;
        }
        else
        {
            bossWillBeSpawned = true;
        }

        playerGO = GameObject.Find("Player");
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

    IEnumerator StartWinCutScene()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("WinCutScene");
    }

    bool winCutSceneScreenTriggered = false;

	// Update is called once per frame
	void Update () {
    
        switch (gameState) {
            //Enemy battle state
            case 0:
                if (bossWillBeSpawned)
                {
                    gameTimer += 1f * Time.deltaTime;
                    if (gameTimer > engageBossAfterTime)
                    {
                        SetGameState(1);
                        DeleteEnemies();
                        GameObject.Find("Player").GetComponent<Player_Attack>().playerState = 2;

                        //set player position to be in the stadium
                        GameObject[] playerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawnDuringBoss");
                        float tempDist = 99999999999f;
                        GameObject closestSpawn = null;
                        foreach (GameObject playerSpawn in playerSpawns)
                        {
                            float currentDistCheck = Mathf.Abs(playerSpawn.transform.position.x - playerGO.transform.position.x);
                            if(currentDistCheck <= tempDist) {
                                tempDist = currentDistCheck;
                                closestSpawn = playerSpawn;
                            }
                        }
                        Vector3 newPosition = new Vector3(closestSpawn.transform.position.x, playerGO.transform.position.y, playerGO.transform.position.z);

                        playerGO.transform.position = newPosition;
                        playerGO.GetComponent<Player_Attack>().SetTargetMovePosition(newPosition);

                        // disable the attack icon
                        attackIcon.SetActive(false);
                        // disable static beam icon
                        staticBeamIcon.SetActive(false);
                    }
                }
                
                break;
            //Boss instantiate state
            case 1:
                // turn on cutscene for boss here
                cutScene.StartCutScene();
                bossCutsceneStarted = true;
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

        if (bossHealthScript.isBossDead && !winCutSceneScreenTriggered)
        {
            StartCoroutine(StartWinCutScene());
            winCutSceneScreenTriggered = true;
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
