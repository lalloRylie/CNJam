using UnityEngine;
using System.Collections;

public class Player_MoveAndAttackDuringBossFight : MonoBehaviour
{

    GameStateControl gameStateControlScript;
    GameObject bossGO = null;

    float playerSpeed = 5f;

    float attackRangeAgainstBoss = 1f;

    Vector3 targetPosition = new Vector3(0f, 0f, 0f);

    Player_Attack playerAttackScript;

    float dodgeAttackLength = 1.5f;

    public Player_ControlAnimationState playerAnimScript;

    float playerXScale = 0f;

    public GameObject playerSpriteGO = null;

    [HideInInspector]
    public bool playerCanMove = true;

    public Player_Score playerScoreScript = null;

    BossBehavior bossBehaviorScript = null;

    // Use this for initialization
    void Start()
    {
        playerXScale = playerSpriteGO.transform.localScale.x;
        gameStateControlScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateControl>();
        playerAttackScript = GetComponent<Player_Attack>();
    }

    void LeftAttackBoss()
    {
        // get position of boss, then set the targetMove position for the player to the left of the boss, and player dodge attack animation
        Vector2 bossPosition = bossGO.transform.position;
        bossPosition.y = 0f;
        targetPosition = bossPosition - new Vector2(dodgeAttackLength, 0f);

        //play animation
        playerAnimScript.SetAnimState(1);

        // deal damage to boss
        bossGO.GetComponent<BossTakeDamage>().TakeDamage(1);

        // add one to multiplier
        playerAttackScript.scoreMultiplier++;

        // add points
        playerScoreScript.score += 10 * playerAttackScript.scoreMultiplier;
    }

    void RightAttackBoss()
    {
        // get position of boss, then set the targetMove position for the player to the left of the boss, and player dodge attack animation
        Vector2 bossPosition = bossGO.transform.position;
        bossPosition.y = 0f;
        targetPosition = bossPosition + new Vector2(dodgeAttackLength, 0f);

        //play animation
        playerAnimScript.SetAnimState(1);

        // deal damage to boss
        bossGO.GetComponent<BossTakeDamage>().TakeDamage(1);

        // add one to multiplier
        playerAttackScript.scoreMultiplier++;

        // add points
        playerScoreScript.score += 10 * playerAttackScript.scoreMultiplier;
    }

    void TranslateCharacter()
    {
        Vector2 targetPos = targetPosition - transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerAttackScript.attackStateZeroLerpSpeed);
    }

    BossHealth bossHealthScript = null;
    private Vector2 startPos;
    private Vector2 currentPos;

    // Update is called once per frame
    void Update()
    {
        if (gameStateControlScript.gameState != 2)
        {
            return;
        }

        if (bossGO == null)
        {
            bossGO = GameObject.FindGameObjectWithTag("Boss");
            targetPosition = transform.position;
            return;
        }

        if (bossGO == null) return;

        if (bossBehaviorScript == null) bossBehaviorScript = bossGO.GetComponent<BossBehavior>();

        if (bossBehaviorScript == null) return;
        if (!playerCanMove) return;

        TranslateCharacter();

        if (bossHealthScript == null) bossHealthScript = bossGO.GetComponent<BossHealth>();


        if (!bossHealthScript.isBossDead)
        {

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // is boss on ground?
                if (bossBehaviorScript.bossOnGround)
                {
                    // then get distance from boss
                    float xDist = bossGO.transform.position.x - transform.position.x;
                    // if you're close enough, dodge attack the boss, then return so you don't translate
                    if (xDist <= playerAttackScript.attackStateZeroRange * 1.5f && xDist < 0f)
                    {
                        LeftAttackBoss();
                        playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                        return;
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log(bossGO.transform.position.x - transform.position.x);
                // is boss on ground?
                if (bossBehaviorScript.bossOnGround)
                {
                    // then get distance from boss
                    float xDist = bossGO.transform.position.x - transform.position.x;
                    // if you're close enough, dodge attack the boss, then return so you don't translate
                    if (xDist <= playerAttackScript.attackStateZeroRange * 1.5f && xDist > 0f)
                    {
                        RightAttackBoss();
                        playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                        return;
                    }
                }
            }

            // move
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.Translate((-Vector3.right * playerSpeed) * Time.deltaTime);
                targetPosition -= (Vector3.right * playerSpeed) * Time.deltaTime;
                playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                targetPosition += (Vector3.right * playerSpeed) * Time.deltaTime;
                playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                //transform.Translate((Vector3.right * playerSpeed) * Time.deltaTime);
            }
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR 
            //if (Input.touchCount > 0)
            //{
            foreach (Touch playerTouch in Input.touches)
            {

                if (playerTouch.phase == TouchPhase.Began)
                {

                    if (playerTouch.position.x < Screen.width * 0.5f)
                    {
                        // left tap
                        // is boss on ground?
                        if (bossBehaviorScript.bossOnGround)
                        {
                            // then get distance from boss
                            float xDist = bossGO.transform.position.x - transform.position.x;
                            // if you're close enough, dodge attack the boss, then return so you don't translate
                            if (xDist <= playerAttackScript.attackStateZeroRange * 1.5f && xDist < 0f)
                            {
                                LeftAttackBoss();
                                playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                                return;
                            }
                        }
                    }
                    else
                    {
                        // right tap
                        // is boss on ground?
                        if (bossBehaviorScript.bossOnGround)
                        {
                            // then get distance from boss
                            float xDist = bossGO.transform.position.x - transform.position.x;
                            // if you're close enough, dodge attack the boss, then return so you don't translate
                            if (xDist <= playerAttackScript.attackStateZeroRange * 1.5f && xDist > 0f)
                            {
                                RightAttackBoss();
                                playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                                return;
                            }
                        }
                    }
                }
            }

            if (Input.touchCount > 0)
            {
                Touch playerTouchZero = Input.GetTouch(0);

                if (playerTouchZero.phase == TouchPhase.Moved || playerTouchZero.phase == TouchPhase.Stationary)
                {
                    if (playerTouchZero.position.x < Screen.width * 0.5f)
                    {
                        // touching left
                        targetPosition -= (Vector3.right * playerSpeed) * Time.deltaTime;
                        playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                    }
                    else
                    {
                        // touching right
                        targetPosition += (Vector3.right * playerSpeed) * Time.deltaTime;
                        playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                    }
                }
            }
#endif
        }
    }
}
