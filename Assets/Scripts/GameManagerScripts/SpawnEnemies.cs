using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public GameObject robertPrefab;
    public GameObject archibaldPrefab;

    float spawnTimer = 0f;
    float difficultyUpTimer = 0f; 
    float amountOfTimeToWaitToSpawnEnemy = 1.5f;

    public Transform playerTransform;

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Player_TakeDamage>().PlayerTakenDamageEvent += SpawnEnemies_PlayerTakenDamageEvent;
	}

    void SpawnEnemies_PlayerTakenDamageEvent()
    {
        amountOfTimeToWaitToSpawnEnemy = 1.5f;
        difficultyUpTimer = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        if (GetComponent<GameStateControl>().gameState == 0)
        {
            difficultyUpTimer += 1f * Time.deltaTime;
            if (difficultyUpTimer >= 10f)
            {
                difficultyUpTimer = 0f;
                amountOfTimeToWaitToSpawnEnemy = amountOfTimeToWaitToSpawnEnemy * 0.88f;
            }

            spawnTimer += 1f * Time.deltaTime;
            if (spawnTimer >= amountOfTimeToWaitToSpawnEnemy)
            {
                spawnTimer = 0f;
                int randomInt = Random.Range(1, 21);
                int randomIntForEnemyType = Random.Range(1, 21);

                GameObject enemyToSpawn = null;

                if(randomIntForEnemyType < 10) {
                    enemyToSpawn = robertPrefab;
                }
                else
                {
                    enemyToSpawn = archibaldPrefab;
                }

                if (amountOfTimeToWaitToSpawnEnemy >= 1.5f)
                {
                    enemyToSpawn = robertPrefab;
                }

                float yStartPos = -2.25f;

                if (randomInt < 10)
                {
                    // spawn on left side
                    GameObject enemyToBeSpawned = GameObject.Instantiate<GameObject>(enemyToSpawn);
                    Vector3 enemySpawnPos = playerTransform.position;
                    enemySpawnPos.y = yStartPos;
                    enemyToBeSpawned.transform.position = enemySpawnPos + new Vector3((-horzExtent * 1.5f) - 1f, 0.0f, 0f);
                }
                else
                {
                    // spawn on right side
                    GameObject enemyToBeSpawned = GameObject.Instantiate<GameObject>(enemyToSpawn);
                    Vector3 enemySpawnPos = playerTransform.position;
                    enemySpawnPos.y = yStartPos;
                    enemyToBeSpawned.transform.position = enemySpawnPos + new Vector3((horzExtent * 1.5f) + 1f, 0.0f, 0f);
                }
            }
        }
	}


}
