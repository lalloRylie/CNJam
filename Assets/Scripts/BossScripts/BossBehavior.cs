using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    public int bossState = 0;
    public float bossAirSpeed;
    public Vector3 bossStartPos = new Vector3(0f, 5f, 0f);
    bool isGoingLeft = true;
    public float maxPatrolDist = 10f;

    public bool spawnTears = true;

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

    float groundYPos = -2.74f;
    float airYPos = 4.35f;

    float startAirXPos;

    float startXScale = 0f;

    [HideInInspector]
    public bool bossOnGround = false;

    BossHealth bossHealthScript;

    Player_ControlAnimationState playerAnimScript;

    public GameObject bossSpriteGO = null;
    BossAnimationControllerScript bossAnimScript;

    Player_Attack playerAttackScript = null;

    float amountOfAirTime = 8f;

    void Cry()
    {
        if (!spawnTears) return;

        float amountOfTimeToWaitToSpawnEnemy = 0.8f;

        float randXPos = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos = new Vector3(randXPos, transform.position.y - 1f, 0f);

        cryTimer += 1f * Time.deltaTime;
        if (cryTimer >= amountOfTimeToWaitToSpawnEnemy)
        {
            GameObject tear = GameObject.Instantiate<GameObject>(tearPrefab);
            tear.transform.position = spawnPos;
            tear.GetComponent<RunTear>().fallSpeed = Random.Range(2f, 4f);
            cryTimer = 0;
        }
    }

    void CryMore()
    {
        if (!spawnTears) return;

        float amountOfTimeToWaitToSpawnEnemy = 0.4f; //Should be random/varying

        float randXPos1 = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos1 = new Vector3(randXPos1, transform.position.y - 1f, 0f);

        float randXPos2 = Random.Range(transform.position.x - (tearSpawnZoneWidth / 2f), transform.position.x + (tearSpawnZoneWidth / 2f));
        Vector3 spawnPos2 = new Vector3(randXPos2, transform.position.y - 1f, 0f);

        //Compare spawn locations of both objects, if they're too close, move them away from eachother.
        if((Mathf.Abs(spawnPos1.x) - Mathf.Abs(spawnPos2.x)) < 1.0f)
        {
            float offset = 2.0f;
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
            tear1.GetComponent<RunTear>().fallSpeed = Random.Range(2f, 4f);

            GameObject tear2 = GameObject.Instantiate<GameObject>(tearPrefab);
            tear2.transform.position = spawnPos2;
            tear2.GetComponent<RunTear>().fallSpeed = Random.Range(2f, 5f);

            cryTimer = 0;
        }
    }

    void ChangeDirections()
    {
        isGoingLeft = !isGoingLeft;

        if(!isGoingLeft) {
            bossSpriteGO.transform.localScale = new Vector3(-startXScale, bossSpriteGO.transform.localScale.y, bossSpriteGO.transform.localScale.z);
        }
        else
        {
            bossSpriteGO.transform.localScale = new Vector3(startXScale, bossSpriteGO.transform.localScale.y, bossSpriteGO.transform.localScale.z);
        }
    }

    void BossAirMovement()
    {
        bossAnimScript.SetBossAnimState(0);
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

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossStartPos = new Vector3(player.transform.position.x, bossStartPos.y, bossStartPos.z);
        
        transform.position = bossStartPos;
        
        bossHealthScript = GetComponent<BossHealth>();

        startAirXPos = transform.position.x;

        playerAnimScript = player.GetComponentInChildren<Player_ControlAnimationState>();

        startXScale = bossSpriteGO.transform.localScale.x;

        bossAnimScript = bossSpriteGO.GetComponent<BossAnimationControllerScript>();
        playerAttackScript = player.GetComponent<Player_Attack>();

        airYPos = bossStartPos.y;
    }

    IEnumerator TriggerEMP()
    {
        //Debug.Log("emp triggered");
        playerAnimScript.SetAnimState(5);

        yield return new WaitForSeconds(0.8f);

        bossAnimScript.SetBossAnimState(1);
        if (bossState == -1) bossState = 1;
        else if (bossState == -5) bossState = 5;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            bossHealthScript.health = 0;
        }

        if (bossHealthScript.isBossDead) return;
        // fly around and shoot tears, after some time, we'll get emp'd by sparko, causing crybaby to fall
        if (bossState == 0)
        {
            bossTimer += 1.0f * Time.deltaTime;
            float chargeAmount = DataCore.Remap(bossTimer, 0f, amountOfAirTime, 0f, 30f);
            playerAttackScript.playerCharge = chargeAmount;
            
            if(bossTimer >= amountOfAirTime) {
                bossTimer = 0f;
                bossState = -1;
                StartCoroutine(TriggerEMP());

                bossAirSpeed += 2;
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
                //Debug.Log("on ground");
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
                //Debug.Log("Going Up");
                bossAnimScript.SetBossAnimState(2);

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
               // Debug.Log("in air");
            }
        }

        if(bossState == 4) {
            // cry more!
            bossTimer += 1.0f * Time.deltaTime;
            float chargeAmount = DataCore.Remap(bossTimer, 0f, amountOfAirTime, 0f, 30f);
            playerAttackScript.playerCharge = chargeAmount;

            if (bossTimer >= amountOfAirTime)
            {
                bossTimer = 0f;
                bossState = -5;
                StartCoroutine(TriggerEMP());
            }

            BossAirMovement();
            CryMore();
        }

        if(bossState == 5) {
            bossAnimScript.SetBossAnimState(1);
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
               // Debug.Log("on ground");
            }
        }

        if (bossState == 6)
        {
            // wait for a certain amount of time, or after crybaby takes a certain amount of damage

            //Debug.Log(bossHealthScript.startHealth - (bossHealthScript.startHealth / 2));
            bossTimer += 1f * Time.deltaTime;
            if (bossTimer >= 2f)
            {
                bossAnimScript.SetBossAnimState(2);
                bossOnGround = false;
                bossState = 7;
                bossTimer = 0f;
                //Debug.Log("Going Up");
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
                //Debug.Log("in air");
            }
        }

    }
}