using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public GameObject player;
    public int enemyState = 1;
    private float attackTimer = 0.0f;
    public float timeToWait = 2.0f;
    public float enemySpeed = 1.0f;
    public float attackRange = 2.0f;
    private float distToPlayer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        distToPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);

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
                    //Run attack animation
                } else if (distToPlayer > attackRange * 1.5f)
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
