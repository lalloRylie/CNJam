using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public GameObject player;
    public int enemyState = 1;
    private float attackTimer = 0.0f;
    public float timeToWait = 1.0f;
    public float enemySpeed = 1.0f;
    public float attackRange = 2.0f;
    float minDistanceFromPlayer; //Set from inspector 2.5 for Archi, 1.4 for Robert
    private float distToPlayer;

    //Check position relative to player, make sure enemy is always facing the player
    void CheckDirection()
    {
        if (transform.position.x < player.transform.position.x)
        {
            //enemy is to the left of the player and needs to be flipped (localScale.x should be negative)
            if (transform.localScale.x < 0)
            {
                //image has already been flipped
                return;
            }
            else
            {
                //flip enemy to face player
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }         
        }
        else
        {
            //enemy is to the right of the player and doesn't need to be flipped (localScale.x should be positive)
            if (transform.localScale.x > 0)
            {
                //image hasn't been flipped
                return;
            }
            else
            {
                //flip enemy to face player
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    ////If the enemy is too close to player, move them back slightly to eliminate collision bugs
    //void CheckPosition() {
    //    float currentDistFromPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);

    //    if (currentDistFromPlayer < minDistanceFromPlayer) {
    //        //enemy is too close, move them back to just within their attack range.

    //        //check which direction to move enemy
    //        if (transform.position.x < player.transform.position.x)
    //            transform.position = new Vector3(player.transform.position.x - minDistanceFromPlayer, transform.position.y, transform.position.z);
    //        else
    //            transform.position = new Vector3(player.transform.position.x + minDistanceFromPlayer, transform.position.y, transform.position.z);
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        distToPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);
        //CheckPosition();
        CheckDirection();
        
        switch (enemyState)
        {
            //Idle state ?? maybe ??
            case 0:
                break;
            //Moving state
            case 1:
                attackTimer = 0;
                //Play enemy moving animation
                Vector3 newPos = player.transform.position;
                float step = enemySpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, newPos, step);

                //If within (attack range), move to waiting state
                if (distToPlayer < attackRange)
                {
                    Debug.Log("Enemy is in range to attack!");
                    enemyState = 2;
                }
                break;
            //Waiting to attack state
            case 2:
                attackTimer += 1.0f * Time.deltaTime;
                //If timer > timeToWait, enter attacking state
                if (attackTimer > timeToWait && distToPlayer < attackRange)
                {
                    Debug.Log("Enemy is attacking!");
                    enemyState = 3;
                }
                else if (distToPlayer > attackRange * 1.5f)
                {
                    Debug.Log("You managed to get away!");
                    enemyState = 1;
                }
                break;
            //Attacking state
            case 3:
                //If enemy is within (attack range + tolerance) of the player run player.TakeDamage(x damage) 
                if (distToPlayer < (attackRange + attackRange / 10.0f))
                {
                    Debug.Log("You took damage!");
                    player.GetComponent<Player_TakeDamage>().TakeDamage(1);
                    enemyState = 1;
                    //Run attack animation
                }
                else if(distToPlayer > attackRange * 1.5f) 
                {
                    enemyState = 1;
                }
                break;
            //Death state
            case 4:
                //Run any destroy functions needed to delete current enemy
                //Play death animation
                    //Fall over, flash red (some other color)
                Destroy(gameObject);
                break;
            default:
                enemyState = 1;
                break;
        }
    }
}
