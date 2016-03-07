using UnityEngine;
using System.Collections;

public class RunTear : MonoBehaviour
{

    public float fallSpeed = 3f;
    [HideInInspector]
    public Player_TakeDamage playerTakeDamageScript;
    public Player_Attack playerAttackScript;

    public GameObject tearParticles = null;
    public GameObject tearHitGroundSFX = null;

    GameObject groundPlaneForParticles = null;

    GameObject playerGO = null;

    // Use this for initialization
    void Start()
    {
        groundPlaneForParticles = GameObject.FindWithTag("ParticlesGroundPlane");
        playerGO = GameObject.Find("Player");
        playerTakeDamageScript = playerGO.GetComponent<Player_TakeDamage>();
        playerAttackScript = playerGO.GetComponent<Player_Attack>();
        
    }

    // Update is called once per frame
    void Update()
    {
        fallSpeed += 20.0f * Time.deltaTime;

        transform.Translate(new Vector3(0f, -fallSpeed * Time.deltaTime));

        if (transform.position.y <= groundPlaneForParticles.transform.position.y)
        {
            Vector3 spawnPos = groundPlaneForParticles.transform.position;
            spawnPos.x = transform.position.x;
            spawnPos.y += 0.1f;

            Instantiate(tearParticles, spawnPos, Quaternion.identity);
            Instantiate(tearHitGroundSFX, spawnPos, Quaternion.identity);

            Destroy(gameObject);
        }
            
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {

            if (playerTakeDamageScript.playerTakeDamageDelayTimer >= 0f) return;

            if (playerTakeDamageScript.canTakeDamage)
            {
                Vector3 spawnPos = playerGO.transform.position;
                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y + 0.1f;

                Instantiate(tearParticles, spawnPos, Quaternion.identity);
                Instantiate(tearHitGroundSFX, spawnPos, Quaternion.identity);

                playerTakeDamageScript.playerHealth -= 1;
                playerTakeDamageScript.Trigger_PlayerTakenDamageEvent();
                playerAttackScript.DeductScoreMultiplier(true);
                playerTakeDamageScript.playerTakeDamageDelayTimer = playerTakeDamageScript.amountOfDelayAfterTakingDamage;
            }

            Destroy(gameObject);
        }
    }
}
