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

    public BossFloatSprite bossFloatSpriteScript = null;

    float fallSpeed = 2f;
    float startingFallSpeed = 2f;

    float groundYPos = -2.35f;
    float airYPos = 4.35f;

    float startAirXPos;

    [HideInInspector]
    public bool bossOnGround = false;

    BossHealth bossHealthScript;

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
        float distFromStart = transform.position.x - player.transform.position.x;

        if (isGoingLeft)
        {
            if (distFromStart < -maxPatrolDist)
                ChangeDirections();

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
        player = GameObject.FindGameObjectWithTag("Player");
        bossStartPos = new Vector3(player.transform.position.x, bossStartPos.y, bossStartPos.z);
        
        transform.position = bossStartPos;
        
        bossHealthScript = GetComponent<BossHealth>();

        startAirXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // fly around and shoot tears, after some time, we'll get emp'd by sparko, causing crybaby to fall
        if (bossState == 0)
        {
            bossTimer += 1.0f * Time.deltaTime;
            if(bossTimer >= 3f) {
                bossTimer = 0f;
                bossState = 1;
            }

            BossAirMovement();
            Cry();
        }
        if (bossState == 1)
        {
            // fall towards ground
            transform.Translate((Vector3.down * fallSpeed) * Time.deltaTime);

            // add gravity to fall
            fallSpeed += 30f * Time.deltaTime;

            // turn off float script
            bossFloatSpriteScript.enabled = false;

            if (transform.position.y <= groundYPos)
            {
                fallSpeed = startingFallSpeed;
                bossState = 2;
                bossOnGround = true;
                Debug.Log("on ground");
            }
        }

        if (bossState == 2)
        {
            // wait for a certain amount of time, or after crybaby takes a certain amount of damage

            //Debug.Log(bossHealthScript.startHealth - bossHealthScript.startHealth / 3);
            bossTimer += 1f * Time.deltaTime;
            if (bossTimer >= 5f || bossHealthScript.health < bossHealthScript.startHealth - (bossHealthScript.startHealth / 3))
            {
                bossState = 3;
                bossTimer = 0f;
                bossOnGround = false;
                Debug.Log("Going Up");
            }
            //BossGroundBehavior();
        }

        if (bossState == 3)
        {
            // rise up in the air
            transform.Translate((Vector3.up * fallSpeed) * Time.deltaTime);

            // add acceleration to rise
            fallSpeed += 30f * Time.deltaTime;

            if (transform.position.y >= airYPos)
            {
                fallSpeed = startingFallSpeed;
                bossState = 4;
                // turn on float script
                bossFloatSpriteScript.enabled = true;
                Debug.Log("in air");
            }
        }

        if(bossState == 4) {
            // cry more!
            bossTimer += 1.0f * Time.deltaTime;
            if (bossTimer >= 3f)
            {
                bossTimer = 0f;
                bossState = 5;
            }

            BossAirMovement();
            CryMore();
        }

        if(bossState == 5) {
            // fall towards ground
            transform.Translate((Vector3.down * fallSpeed) * Time.deltaTime);

            // add gravity to fall
            fallSpeed += 30f * Time.deltaTime;

            // turn off float script
            bossFloatSpriteScript.enabled = false;

            if (transform.position.y <= groundYPos)
            {
                fallSpeed = startingFallSpeed;
                bossState = 6;
                bossOnGround = true;
                Debug.Log("on ground");
            }
        }

        if (bossState == 6)
        {
            // wait for a certain amount of time, or after crybaby takes a certain amount of damage

            Debug.Log(bossHealthScript.startHealth - (bossHealthScript.startHealth / 2));
            bossTimer += 1f * Time.deltaTime;
            if (bossTimer >= 3f)
            {
                bossOnGround = false;
                bossState = 7;
                bossTimer = 0f;
                Debug.Log("Going Up");
            }
        }

        if (bossState == 7)
        {
            // rise up in the air
            transform.Translate((Vector3.up * fallSpeed) * Time.deltaTime);

            // add acceleration to rise
            fallSpeed += 30f * Time.deltaTime;

            if (transform.position.y >= airYPos)
            {
                fallSpeed = startingFallSpeed;
                bossState = 4;
                // turn on float script
                bossFloatSpriteScript.enabled = true;
                Debug.Log("in air");
            }
        }

    }
}