using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    /*
    BOSS FIGHT:
    Cry Baby
        
    Mechanics:
    STAGE 1: (You can see Mega Football Baby tied up in the background, maybe just cutscene?)
        Cry Baby is hovering over the battlefield with his jetpack, raining tears down on Sparko. The rate at which the tears fall will increase over time. 
        Instead of tapping left and right to attack, the taps will translate into left or right movement. Sparko must dodge tears until he has charged enough to release an EMP 
        blast to knock Cry Baby out of the sky. 
    STAGE 2 :
        Cry Baby's falls to Sparko’s feet. Quick! Punch him before his jetpack regains power! After 4 hits or x seconds Cry Baby takes to the air again.
    STAGE 3:
    	Same as STAGE 1;
    STAGE 4:
    	Same as STAGE 2, will go back up into the air if he hasn’t been hit 7 times.
        
    ** Enter cutscene, that shows Cry Baby getting frustrated and calling in an army of evil robots to 
    ** swarm Sparko.
    ** Sparko gets angry?! and is shown rising further into the air and a quicktime event (swipe up)   
    ** pops up. Sparko releases his EMP blasts which takes out the robot army
    ** and Cry Baby in one shot. 
    **
    ** Show something about Sparko saving Mega Football Baby. Player Wins.
    */

    //Boss hover height ~= 5.
    //Patrol between x -10 to 10
    //Randomly spawn projectiles (bombs) in a random location within some zone (collider), so bomb's aren't as predictable

    public int bossState = 0;
    public float bossAirSpeed;
    public Vector3 bossStartPos = new Vector3(0f, 5f, 0f);
    bool isGoingLeft = true;
    public float maxPatrolDist = 10f;

    public GameObject player;

    public GameObject tearPrefab;
    float cryTimer = 0f;

    public float tearSpawnZoneWidth;

    //Use to change boss states
    float bossTimer = 0f;
    float bossGroundPhaseAfterTime = 10f;
    float bossAirPhaseAfterTime = 10f;

    void Cry()
    {      
        float amountOfTimeToWaitToSpawnEnemy = 1f;

        float randXPos = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos = new Vector3(randXPos, transform.position.y - 1f, 0f);

        cryTimer += 1f * Time.deltaTime;
        if (cryTimer >= amountOfTimeToWaitToSpawnEnemy)
        {
            GameObject tear = GameObject.Instantiate<GameObject>(tearPrefab);
            tear.transform.position = spawnPos;
            tear.GetComponent<tearTest>().fallSpeed = Random.Range(2f, 4f);
            cryTimer = 0;
        }
    }

    void CryMore()
    {
        float amountOfTimeToWaitToSpawnEnemy = 1f; //Should be random/varying

        float randXPos1 = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos1 = new Vector3(randXPos1, transform.position.y - 1f, 0f);

        float randXPos2 = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos2 = new Vector3(randXPos2, transform.position.y - 1f, 0f);

        //Compare spawn locations of both objects, if they're too close, move them away from eachother.
        if((Mathf.Abs(spawnPos1.x) - Mathf.Abs(spawnPos2.x)) < 1.0f)
        {
            float offset = 1.0f;
            if (spawnPos1.x < spawnPos2.x)
            {
                spawnPos1.x -= offset;
                spawnPos2.x += offset;
            }
            else
            {
                spawnPos1.x += offset;
                spawnPos2.x -= offset;
            }
            
        }

        cryTimer += 1f * Time.deltaTime;
        if (cryTimer >= amountOfTimeToWaitToSpawnEnemy)
        {
            GameObject tear1 = GameObject.Instantiate<GameObject>(tearPrefab);
            tear1.transform.position = spawnPos1;
            tear1.GetComponent<tearTest>().fallSpeed = Random.Range(2f, 4f);

            GameObject tear2 = GameObject.Instantiate<GameObject>(tearPrefab);
            tear2.transform.position = spawnPos2;
            tear2.GetComponent<tearTest>().fallSpeed = Random.Range(2f, 5f);

            cryTimer = 0;
        }
    }

    void ChangeDirections()
    {
        isGoingLeft = !isGoingLeft;
    }

    void BossAirMovement()
    {
        float distFromStart = transform.position.x - bossStartPos.x;

        if (isGoingLeft)
        {
            if (distFromStart < -maxPatrolDist)
                ChangeDirections();
            //Add in some sine wave movement to the y translate to give the boss the appearance of floating
            transform.Translate(new Vector3(-bossAirSpeed * Time.deltaTime, 0f, 0f));
        }
        else
        {
            if (distFromStart > maxPatrolDist)
                ChangeDirections();

            transform.Translate(new Vector3(bossAirSpeed * Time.deltaTime, 0f, 0f));
        }
    }

    //Turn on enemyspawner
    void SpawnEnemies() {

    }

    void BossGroundBehavior() {
        
    }

    // Use this for initialization
    void Start()
    {
        transform.position = bossStartPos;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossState)
        {
            //Air stage
            case 0:
                BossAirMovement();
                Cry();
                break;
            case 1:
                BossAirMovement();
                CryMore();
                break;
            //Ground stage
            case 2:
                BossGroundBehavior();
                break;
            //Death - Enter comic book cut scene
            case 3:
                break;
        }
    }
}