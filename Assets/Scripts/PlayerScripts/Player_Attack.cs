﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Attack : MonoBehaviour
{
    public delegate void OnPlayerMissedDelegate();
    public event OnPlayerMissedDelegate OnPlayerMissedEvent;
    public void Trigger_OnPlayerMissedEvent() { if (OnPlayerMissedEvent != null) OnPlayerMissedEvent(); }

    float timerForDodgeAttackBackRayDoubleLength = 0f;
    public bool lastAttackDirectionWasLeft = false;
    float rangeDisplayLength = 0f;

    public GameObject playerSpriteGO;
    float playerXScale = 0f;

    float range = 1.6f;
    public float attackStateZeroRange = 1.6f;
    float attackStateOneRange = 3f;
    float attackStateTwoRange = 5f;

    float offset = 0.6f;

    int attackState = 1;
    public int playerState = 1;

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
    int attacksLandedBeforeAllowingHalfBoardWipe = 15;
    int attacksLandedBeforeAllowingFullBoardWipe = 30;

    Rigidbody2D playerRB;

    float lerpSpeed = 10f;

    public float attackStateZeroLerpSpeed = 20f;
    float missLerpSpeed = 10f;

    bool halfBoardWipeUsed = false;

    public bool halfBoardWipeSideOnLeft = false;

    public bool playerCanMove = true;

    public float playerCharge = 0f;
    public float maxCharge = 30f;

    // Use this for initialization
    void Start()
    {
        rangeDisplayLength = attackStateZeroRange;
        playerRB = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        playerXScale = playerSpriteGO.transform.localScale.x;
    }

    public void SetPlayerState(int state)
    {
        playerState = state;
    }

    public void DeductScoreMultiplier(bool takenDamage)
    {
        DeductCharge();

        halfBoardWipeUsed = false;

        if(takenDamage) {
            scoreMultiplier = (int)(scoreMultiplier * 0.5f);
        }
        else
        {
            scoreMultiplier = (int)(scoreMultiplier * 0.75f);
        }

    }

    void DeductCharge()
    {
        halfBoardWipeUsed = false;

        if (playerCharge >= 20 &&
            playerCharge < 30)
        {
            playerCharge = 10;
            return;
        }

        else if (playerCharge >= 0 &&
                 playerCharge < 20)
        {
            playerCharge = 0;
            return;
        }

    }

    void PlayerMissedRight()
    {
        Trigger_OnPlayerMissedEvent();
        lerpSpeed = missLerpSpeed;
       // Debug.Log("Right Miss");
        attacks.Clear();
        targetPosition = transform.position + new Vector3(missMoveDistance, 0f, 0f);
        attackMissDelayTimer = attackDelayAfterMiss;

        DeductScoreMultiplier(false);
    }

    void PlayerMissedLeft()
    {
        Trigger_OnPlayerMissedEvent();
        lerpSpeed = missLerpSpeed;
        //Debug.Log("Left Miss");
        attacks.Clear();
        targetPosition = transform.position + new Vector3(-missMoveDistance, 0f, 0f);
        attackMissDelayTimer = attackDelayAfterMiss;

        DeductScoreMultiplier(false);
        
    }

    void PlayerLeftHit(GameObject enemy, float hitDistance, float colliderWidth)
    {
        scoreMultiplier++;

        // dodge attacks
        if (attackState == 0)
        {
            // tell enemy to take damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(1);
            lerpSpeed = attackStateZeroLerpSpeed;
            targetPosition = enemy.transform.position - new Vector3(colliderWidth, 0f, 0f);
        }
        // punch attacks
        else if (attackState == 1)
        {
            // tell enemy to take damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(3);
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position - new Vector3(hitDistance, 0f, 0f);
        }
        // shock attacks
        else if (attackState == 2)
        {
            // tell enemy to take more damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(2);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(4);
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position - new Vector3(hitDistance * 0.5f, 0f, 0f);
        }
    }

    void PlayerRightHit(GameObject enemy, float hitDistance, float colliderWidth)
    {
        scoreMultiplier++;
        
        // dodge attacks
        if (attackState == 0)
        {
            // tell enemy to take damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(1);
            lerpSpeed = attackStateZeroLerpSpeed;
            targetPosition = enemy.transform.position + new Vector3(colliderWidth, 0f, 0f);
        }
        // punch attacks
        else if (attackState == 1)
        {
            // tell enemy to take damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(1);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(3);
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position + new Vector3(hitDistance, 0f, 0f);
        }
        // shock attacks
        else if (attackState == 2)
        {
            // tell enemy to take more damage
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(2);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(4);
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position + new Vector3(hitDistance * 0.5f, 0f, 0f);
        }
    }

    public void BoardWipeEMP()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // tell each enemy to go to death state
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(10);
        }

        playerCharge = 0;
        halfBoardWipeUsed = false;
    }

    public void HalfBoardWipe(bool left)
    {
        // playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(2);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            //If the enemy is on the left half of the screen
            //Clear them
            if (left)
            {
                if (Camera.main.WorldToScreenPoint(enemy.transform.position).x <= Screen.width / 2)
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

        halfBoardWipeUsed = true;
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

        if (playerCharge >= attacksLandedBeforeAllowingFullBoardWipe)
        {
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(5);
            //BoardWipeEMP();
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
                        lastAttackDirectionWasLeft = true;
                        playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                        attacks.Add(-1);
                    }
                    else if (startPos.x > Screen.width / 2)
                    {
                        //The player tapped right
                        lastAttackDirectionWasLeft = false;
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
                            //HalfBoardWipe(true);
                            //halfBoardWipeSideOnLeft = true;
                            //playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(2);
                        }
                        else
                        {
                            //The player swiped right
                            //HalfBoardWipe(false);
                            //halfBoardWipeSideOnLeft = false;
                            //playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(2);
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

        if (playerCharge >= attacksLandedBeforeAllowingHalfBoardWipe)
        {
            if (!halfBoardWipeUsed)
            {
                if (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)))
                {
                    halfBoardWipeSideOnLeft = false;
                    playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(2);
                    playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                    return;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)))
                {
                    halfBoardWipeSideOnLeft = true;
                    playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(2);
                    playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                    return;
                }
            }
        }

        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastAttackDirectionWasLeft = false;
            playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(1);
        }

        // left
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastAttackDirectionWasLeft = true;
            playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(-1);
        }

#endif

        Debug.DrawRay(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right * range, Color.red);
        Debug.DrawRay(transform.TransformPoint(new Vector3(-offset, 0.0f, 0.0f)), -Vector2.right * range, Color.red);
    }

    void PlayerRightAttack()
    {
        float newRange = range;
        if(attackState == 0 && timerForDodgeAttackBackRayDoubleLength > 0f) {
            newRange *= 2f;
        }
        rangeDisplayLength = newRange;
        RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right, newRange);

        if (hit)
        {
            timerForDodgeAttackBackRayDoubleLength = 0.2f;
            if (hit.collider.tag == "EnemyCollider")
            {
                PlayerRightHit(hit.collider.transform.parent.gameObject, hit.distance, hit.collider.bounds.size.x);
            }
        }
        else
        {
            PlayerMissedRight();
        }
    }

    void PlayerLeftAttack()
    {
        float newRange = range;
        if (attackState == 0 && timerForDodgeAttackBackRayDoubleLength > 0f)
        {
            newRange *= 2f;
        }
        rangeDisplayLength = newRange;
        RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(new Vector3(-offset, 0.0f, 0.0f)), -Vector2.right, newRange);

        if (hit)
        {
            timerForDodgeAttackBackRayDoubleLength = 0.2f;
            if (hit.collider.tag == "EnemyCollider")
            {
                PlayerLeftHit(hit.collider.transform.parent.gameObject, hit.distance, hit.collider.bounds.size.x);
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
               // Debug.Log("Right");
                PlayerRightAttack();
                attacks.Remove(attack);
                return;
            }
            else if (attack == -1)
            {
                //Debug.Log("Left");
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
        // shocks
        if (playerCharge >= 20 &&
            playerCharge < 30)
        {
            attackState = 2;
            range = attackStateTwoRange;
           // Debug.Log("attack state = 2");
            return;
        }

        // punches
        else if (playerCharge >= 10 &&
                 playerCharge < 20)
        {
            attackState = 1;
            range = attackStateOneRange;
            //Debug.Log("attack state = 1");
            return;
        }

        else if (playerCharge >= 0 &&
                 playerCharge < 10)
        {
            attackState = 0;
            range = attackStateZeroRange;
           // Debug.Log("attack state = 0");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCanMove) return;

        switch (playerState) {
            //Idle state - do nothing
            case 0:
                break;
            //Normal battle state
            case 1:
                timerForDodgeAttackBackRayDoubleLength -= 1f * Time.deltaTime;

                RunAttackStateChange();

                MovePlayer();

                InitiatePlayerAttack();

                HandlePlayerAttacks();
                break;
            //Boss battle state
            case 2:
                break;
            //Lose state
            case 3:
                break;
        }

        
    }
}
