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
        if(attackState == 0) {
            playerSpriteGO.GetComponent<Player_ControlAnimationState>().SetAnimState(1);
            //GetComponent<Player_ControlAnimationState>().SetAnimState(0);
            lerpSpeed = attackStateZeroLerpSpeed;
            targetPosition = transform.position - new Vector3(missMoveDistance*3f, 0f, 0f);
        }
        else if(attackState == 1) {
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
            targetPosition = transform.position + new Vector3(missMoveDistance *3f, 0f, 0f);
        }
        else if (attackState == 1)
        {
            lerpSpeed = missLerpSpeed;
            targetPosition = transform.position + new Vector3(hitDistance, 0f, 0f);
        }
    }

    void BoardWipeEMP()
    {
        scoreMultiplier = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            // tell each enemy to go to death state
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(10);
        }
    }

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

        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerSpriteGO.transform.localScale = new Vector3(-playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(1);
        }

        Debug.DrawRay(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right * range, Color.red);

        // left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
            attacks.Add(-1);
        }

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
