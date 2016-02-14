﻿using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public GameObject robertPrefab;
    public GameObject archibaldPrefab;

    float spawnTimer = 0f;
    float amountOfTimeToWaitToSpawnEnemy = 1.5f;

    public Transform playerTransform;

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        if (!player.GetComponent<Player_TakeDamage>().playerDead)
        {
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

                if (randomInt < 10)
                {
                    // spawn on left side
                    GameObject enemyToBeSpawned = GameObject.Instantiate<GameObject>(enemyToSpawn);
                    enemyToBeSpawned.transform.position = playerTransform.position + new Vector3((-horzExtent * 1.25f) - 1f, 0f, 0f);
                }
                else
                {
                    // spawn on right side
                    GameObject.Instantiate<GameObject>(enemyToSpawn).transform.position = playerTransform.position + new Vector3((horzExtent * 1.25f) + 1f, 0f, 0f);
                }
            }
        }
        else
        {
            //Open lose screen
            Debug.Log("You lose.");
        }
        
	}
}
