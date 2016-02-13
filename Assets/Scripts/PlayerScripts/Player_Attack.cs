using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Attack : MonoBehaviour
{

    public GameObject playerSpriteGO;
    float playerXScale = 0f;

    float range = 1.5f;
    float offset = 0.6f;

    int attackState = 0;

    Vector3 targetPosition = new Vector3(0f, 0f, 0f);

    List<int> attacks = new List<int>();

    float missMoveDistance = 1.5f;
    //float missMoveDistance = 15f;

    [HideInInspector]
    public int attackHitCounter = 0;
    [HideInInspector]
    public int scoreMultiplier = 0;

    float attackMissDelayTimer = 0.0f;

    float attackDelayAfterMiss = .5f;

    int attacksLandedBeforeGoingToSecondAttackState = 10;
    int attacksLandedBeforeGoingToThirdAttackState = 20;
    int attacksLandedBeforeAllowingHalfBoardWipe = 25;
    int attacksLandedBeforeAllowingFullBoardWipe = 30;

    Rigidbody2D playerRB;

    float lerpSpeed = 10f;

    float attackStateZeroLerpSpeed = 20f;
    float missLerpSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        playerXScale = playerSpriteGO.transform.localScale.x;
    }

    void DeductScoreMultiplier()
    {
        if (scoreMultiplier > attacksLandedBeforeAllowingHalfBoardWipe &&
            scoreMultiplier < attacksLandedBeforeAllowingFullBoardWipe)
        {
            scoreMultiplier = attacksLandedBeforeGoingToThirdAttackState;
            return;
        }

        else if (scoreMultiplier > attacksLandedBeforeGoingToThirdAttackState &&
                 scoreMultiplier < attacksLandedBeforeAllowingHalfBoardWipe)
        {
            scoreMultiplier = attacksLandedBeforeGoingToSecondAttackState;
            return;
        }

        else if (scoreMultiplier > attacksLandedBeforeGoingToSecondAttackState &&
                 scoreMultiplier < attacksLandedBeforeGoingToThirdAttackState)
        {
            scoreMultiplier = 0;
            return;
        }

        else if (scoreMultiplier > 0 &&
                 scoreMultiplier < attacksLandedBeforeGoingToSecondAttackState)
        {
            scoreMultiplier = 0;
            return;
        }

    }

    void PlayerMissedRight()
    {
        lerpSpeed = missLerpSpeed;
        Debug.Log("Right Miss");
        attacks.Clear();
        targetPosition = transform.position + new Vector3(missMoveDistance, 0f, 0f);
        attackMissDelayTimer = attackDelayAfterMiss;

        DeductScoreMultiplier();
    }

    void PlayerMissedLeft()
    {
        lerpSpeed = missLerpSpeed;
        Debug.Log("Left Miss");
        attacks.Clear();
        targetPosition = transform.position + new Vector3(-missMoveDistance, 0f, 0f);
        attackMissDelayTimer = attackDelayAfterMiss;

        DeductScoreMultiplier();
    }

    void PlayerLeftHit(GameObject enemy, float hitDistance)
    {
        scoreMultiplier++;
        // tell enemy to take damage
        enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);

        // set player target position
        if (attackState == 0)
        {
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(1);
            //GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            lerpSpeed = attackStateZeroLerpSpeed;
            targetPosition = transform.position - new Vector3(missMoveDistance * 2f, 0f, 0f);
        }
        else if (attackState == 1)
        {
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position - new Vector3(hitDistance, 0f, 0f);
        }

    }

    void PlayerRightHit(GameObject enemy, float hitDistance)
    {
        scoreMultiplier++;
        // tell enemy to take damage

        enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);

        // set player target position
        if (attackState == 0)
        {
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(1);
            //GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            lerpSpeed = attackStateZeroLerpSpeed;
            targetPosition = transform.position + new Vector3(missMoveDistance * 2f, 0f, 0f);
        }
        else if (attackState == 1)
        {
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position + new Vector3(hitDistance, 0f, 0f);
        }
    }

    public void BoardWipeEMP()
    {
        scoreMultiplier = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // tell each enemy to go to death state
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(10);
        }
    }

    void HalfBoardWipe(bool left)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            //If the enemy is on the left half of the screen
            //Clear them
            if (left)
            {
                if(Camera.main.WorldToScreenPoint(enemy.transform.position).x <= Screen.width / 2)
                    enemy.GetComponent<EnemyTakeDamage>().TakeDamage(10);
            }
            //If the enemy is on the right half of the screen
            //Clear them
            else 
            {
                if (Camera.main.WorldToScreenPoint(enemy.transform.position).x >= Screen.width / 2)
                    enemy.GetComponent<EnemyTakeDamage>().TakeDamage(10);
            }
                    
        }

    }

    public float minSwipeDist;
    private Vector2 startPos;

    //Swivel
    private Vector2 currentPos;
    private Vector2 lastPos;
    private Vector2 vectorDir;
    float worldScreenHeight;
    float worldScreenWidth;

    void InitiatePlayerAttack()
    {
        attackMissDelayTimer -= 1.0f * Time.deltaTime;

        if (attackMissDelayTimer > 0f)
        {
            return;
        }

        if (scoreMultiplier >= attacksLandedBeforeAllowingFullBoardWipe)
        {
            BoardWipeEMP();
            return;
        }

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR 

        if (Input.touchCount > 0)
        {
            Touch playerTouch = Input.GetTouch(0);
            currentPos = playerTouch.position;

            switch (playerTouch.phase)
            {
                case TouchPhase.Began:
                    startPos = playerTouch.position;

                    //TAP
                    if (startPos.x < Screen.width / 2)
                    {
                        //The player tapped left
                        playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                        attacks.Add(-1);
                    }
                    else if (startPos.x > Screen.width / 2)
                    {
                        //The player tapped right
                         playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                         attacks.Add(1);
                    }
                    //END TAP

                    break;
                case TouchPhase.Moved:
                    //SWIPE
                    Vector2 swipeVector = playerTouch.position - startPos;
                    float swipeVectorMag = Mathf.Sqrt(swipeVector.x * swipeVector.x + swipeVector.y * swipeVector.y);

                    //if (Mathf.Abs(swipeVector.x) > minSwipeDist || Mathf.Abs(swipeVector.y) > minSwipeDist)
                    if (swipeVectorMag > minSwipeDist)
                    {
                        if (swipeVector.x < 0)
                        {
                            //The player swiped left
                            HalfBoardWipe(true);
                        }
                        else
                        {
                            //The player swiped right
                            HalfBoardWipe(false);
                        }
                    }
                    //END SWIPE

                    break;
                case TouchPhase.Ended:

                    break;
            }
        }



#endif

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER


        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(1);
        }

        // left
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(-1);
        }

        //FIX: Half Board wipe works. Need to get the if statement for the multi key press implemented
        ////
        //if (Input.GetKeyDown(KeyCode.RightArrow) && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
        //{
        //    HalfBoardWipe(false);
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow) && (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)))
        //{
        //    HalfBoardWipe(true);
        //}
        ////
#endif

        Debug.DrawRay(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right * range, Color.red);
        Debug.DrawRay(transform.TransformPoint(new Vector3(-offset, 0.0f, 0.0f)), -Vector2.right * range, Color.red);
    }

    void PlayerRightAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right, range);

        if (hit)
        {
            if (hit.collider.tag == "EnemyCollider")
            {
                PlayerRightHit(hit.collider.transform.parent.gameObject, hit.distance);
            }
        }
        else
        {
            PlayerMissedRight();
        }
    }

    void PlayerLeftAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(new Vector3(-offset, 0.0f, 0.0f)), -Vector2.right, range);

        if (hit)
        {
            if (hit.collider.tag == "EnemyCollider")
            {
                PlayerLeftHit(hit.collider.transform.parent.gameObject, hit.distance);
            }
        }
        else
        {
            PlayerMissedLeft();
        }
    }

    void HandlePlayerAttacks()
    {
        if (attacks.Count > 0)
        {

            int attack = attacks[0];
            if (attack == 1)
            {
                Debug.Log("Right");
                PlayerRightAttack();
                attacks.Remove(attack);
                return;
            }
            else if (attack == -1)
            {
                Debug.Log("Left");
                PlayerLeftAttack();
                attacks.Remove(attack);
                return;
            }
        }
    }

    void MovePlayer()
    {
        Vector2 targetPos = targetPosition - transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        //Vector2 targetPos = new Vector2(targetPosition.x, targetPosition.y) - playerRB.position;
        //playerRB.position = Vector3.Lerp(playerRB.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    void RunAttackStateChange()
    {
        if (scoreMultiplier > attacksLandedBeforeAllowingHalfBoardWipe &&
            scoreMultiplier < attacksLandedBeforeAllowingFullBoardWipe)
        {
            attackState = 3;
            Debug.Log("attack state = 3");
            return;
        }

        else if (scoreMultiplier >= attacksLandedBeforeGoingToThirdAttackState &&
                 scoreMultiplier < attacksLandedBeforeAllowingHalfBoardWipe)
        {
            attackState = 2;
            Debug.Log("attack state = 2");
            return;
        }

        else if (scoreMultiplier >= attacksLandedBeforeGoingToSecondAttackState &&
                 scoreMultiplier < attacksLandedBeforeGoingToThirdAttackState)
        {
            attackState = 1;
            Debug.Log("attack state = 1");
            return;
        }

        else if (scoreMultiplier >= 0 &&
                 scoreMultiplier < attacksLandedBeforeGoingToSecondAttackState)
        {
            attackState = 0;
            Debug.Log("attack state = 0");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RunAttackStateChange();

        MovePlayer();

        InitiatePlayerAttack();

        HandlePlayerAttacks();
    }
}
