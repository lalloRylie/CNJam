using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy1Prefab;

    float spawnTimer = 0f;
    float amountOfTimeToWaitToSpawnEnemy = 1.5f;

    public Transform playerTransform;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        spawnTimer += 1f * Time.deltaTime;
        if(spawnTimer >= amountOfTimeToWaitToSpawnEnemy) {
            spawnTimer = 0f;
            int randomInt = Random.Range(1, 21);
            if(randomInt < 10) {
                // spawn on left side
                GameObject.Instantiate<GameObject>(enemy1Prefab).transform.position = playerTransform.position + new Vector3((-horzExtent * 1.25f) - 1f, -1f, 0f);
            }
            else
            {
                // spawn on right side
                GameObject.Instantiate<GameObject>(enemy1Prefab).transform.position = playerTransform.position + new Vector3((horzExtent * 1.25f) + 1f, -1f, 0f);
            }
        }
	}
}
