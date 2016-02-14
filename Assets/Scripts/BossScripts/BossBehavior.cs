using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    /*
    BOSS FIGHT
    Cry Baby
    
    Mechanics:
    	Fire off your EMP burst to stop Cry Baby and save you best friend.Must reach a combo multiplier of X.
    
        Multi-State boss fight?
            Rains down tears - dodge 10, unlock EMP burst
    
    
    STAGE 1: (You can see Mega Football Baby tied up within the vehicle?) (Or glowy eyes indicating mind control?)
    Cry Baby is hovering over the battlefield in (a to-be-decided vehicle), raining tears down on Sparko.
    The rate at which the tears fall will increase over time.
    
    //STAGE 2 nixed
        STAGE 2:
        When Sparko dodges x amount of tears, he will have to begin dealing with enemies who will start spawning from the sides, in addition to still having to dodge the tears raining down from above.
        When Sparko eliminates all enemies (x amount), he will be able to use his Special Attack/Ultimate to knock Cry Baby from the sky.

    STAGE 3:
    Cry Baby's vehicle comes down on the right side of the screen and he is ejected	from it. Sparko is set to the left of the screen (locked camera).
    Cry Baby now has a squirt gun which shoots projectiles towards Sparko. Sparko has to dodge the projectiles, gradually closing the distance to Cry Baby.
    
    ** Enter cut scene, that shows Cry Baby getting frustrated and calling in an army of evil robots to swarm Sparko.
    **Sparko gets angry?! and is shown rising further into the air and a quicktime event (swipe up) pops up. Sparko releases his EMP blasts which takes out the robot army
    **and Cry Baby in one shot. 
    **
    **Show something about Sparko saving Mega Football Baby. Player Wins.
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
    float bossShootTimer = 0f;

    public float bossWidth;

    //TODO: Need to lock the camera for the boss fight
    //TODO: Get rid of multiplier for boss fight? Replace it with something else? Cause boss to give a set amount of points (not affected by anything)?

    void Cry()
    {      
        float amountOfTimeToWaitToSpawnEnemy = 1f;

        float randXPos = Random.Range(transform.position.x - (bossWidth / 2f), transform.position.x + (bossWidth / 2f));
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

        float randXPos1 = Random.Range(transform.position.x - (bossWidth / 2f), transform.position.x + (bossWidth / 2f));
        Vector3 spawnPos1 = new Vector3(randXPos1, transform.position.y - 1f, 0f);

        float randXPos2 = Random.Range(transform.position.x - (bossWidth / 2f), transform.position.x + (bossWidth / 2f));
        Vector3 spawnPos2 = new Vector3(randXPos2, transform.position.y - 1f, 0f);

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

    //Use same sprite from bomb for water gun projectile?
    void BossGroundBehavior() {
        transform.position = new Vector3(10f, player.transform.position.y, 0f);
       
        float bossShootRate = 1f; //Want this to be a variable rate, so projectiles come at the player at an inconsistant rate

        bossShootTimer += 1f * Time.deltaTime;
        if (bossShootTimer > bossShootRate)
        {
            bossShootTimer = 0f;
            //Fire projectile
            //Play shoot animation and fire the projectile from that?
        }
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