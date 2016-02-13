using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Attack : MonoBehaviour
{

    float range = 1.5f;
    float offset = 0.5f;

    int attackState = 0;

    Vector3 targetPosition = new Vector3(0f, 0f, 0f);

    List<int> attacks = new List<int>();

    float missMoveDistance = 2.0f;

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

    // Use this for initialization
    void Start()
    {
        targetPosition = transform.position;
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
        Debug.Log("Right Miss");
        attacks.Clear();
        targetPosition = transform.position + new Vector3(missMoveDistance, 0f, 0f);
        attackMissDelayTimer = attackDelayAfterMiss;

        DeductScoreMultiplier();
    }

    void PlayerMissedLeft()
    {
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

        // set player target position
        targetPosition = transform.position - new Vector3(hitDistance, 0f, 0f);
    }

    void PlayerRightHit(GameObject enemy, float hitDistance)
    {
        scoreMultiplier++;
        // tell enemy to take damage

        // set player target position
        targetPosition = transform.position + new Vector3(hitDistance, 0f, 0f);
    }

    void BoardWipe()
    {
        scoreMultiplier = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            // tell each enemy to go to death state
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
            BoardWipe();
            return;
        }

        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attacks.Add(1);
        }

        Debug.DrawRay(transform.TransformPoint(new Vector3(offset, 0.0f, 0.0f)), Vector2.right * range, Color.red);

        // left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        InitiatePlayerAttack();

        HandlePlayerAttacks();
    }
}
