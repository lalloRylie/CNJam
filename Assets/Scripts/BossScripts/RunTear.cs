using UnityEngine;
using System.Collections;

public class RunTear : MonoBehaviour
{

    public float fallSpeed = 3f;
    [HideInInspector]
    public Player_TakeDamage playerTakeDamageScript;
    public Player_Attack playerAttackScript;

    // Use this for initialization
    void Start()
    {
        playerTakeDamageScript = GameObject.Find("Player").GetComponent<Player_TakeDamage>();
        playerAttackScript = GameObject.Find("Player").GetComponent<Player_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        fallSpeed += 20.0f * Time.deltaTime;

        transform.Translate(new Vector3(0f, -fallSpeed * Time.deltaTime));

        if (transform.position.y < -5f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {

            if (playerTakeDamageScript.playerTakeDamageDelayTimer >= 0f) return;

            if (playerTakeDamageScript.canTakeDamage)
            {
                playerTakeDamageScript.playerHealth -= 1;
                playerTakeDamageScript.Trigger_PlayerTakenDamageEvent();
                playerAttackScript.DeductScoreMultiplier(true);
                playerTakeDamageScript.playerTakeDamageDelayTimer = playerTakeDamageScript.amountOfDelayAfterTakingDamage;
            }

            Destroy(gameObject);
        }
    }
}
