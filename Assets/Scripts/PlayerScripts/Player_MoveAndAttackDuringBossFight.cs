﻿using UnityEngine;
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

        targetPosition = bossPosition - new Vector2(dodgeAttackLength, 0f);

        //play animation
        playerAnimScript.SetAnimState(1);

        // deal damage to boss
        bossGO.GetComponent<BossHealth>().health--;
    }

    void RightAttackBoss()
    {
        // get position of boss, then set the targetMove position for the player to the left of the boss, and player dodge attack animation
        Vector2 bossPosition = bossGO.transform.position;

        targetPosition = bossPosition + new Vector2(dodgeAttackLength, 0f);

        //play animation
        playerAnimScript.SetAnimState(1);

        // deal damage to boss
        bossGO.GetComponent<BossHealth>().health--;
    }

    void MovePlayer()
    {
        Vector2 targetPos = targetPosition - transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerAttackScript.attackStateZeroLerpSpeed);
    }

    // Update is called once per frame
    void Update() {
        if (gameStateControlScript.gameState != 1)
        {
            return;
        }

        if (bossGO == null)
        {
            bossGO = GameObject.FindGameObjectWithTag("Boss");
            targetPosition = transform.position;
        }

        if (bossGO == null) return;

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // is boss on ground?
            if(bossGO.GetComponent<BossBehavior>().bossOnGround) {
                // then get distance from boss
                float xDist = bossGO.transform.position.x - transform.position.x;
                // if you're close enough, dodge attack the boss, then return so you don't translate
                if (xDist <= playerAttackScript.attackStateZeroRange*1.5f && xDist < 0f)
                {
                    LeftAttackBoss();
                    playerSpriteGO.transform.localScale = new Vector3(playerXScale, playerSpriteGO.transform.localScale.y, playerSpriteGO.transform.localScale.z);
                    return;
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(bossGO.transform.position.x - transform.position.x);
            // is boss on ground?
            if (bossGO.GetComponent<BossBehavior>().bossOnGround)
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

	}
}