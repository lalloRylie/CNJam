using UnityEngine;
using System.Collections;

public class EnemyRunAnimations : MonoBehaviour {

    public Animator enemyAnim = null;
    public GameObject collider = null;

    public GameObject robotDeathSFX;

    Player_Score playerScoreScript = null;
    Player_Attack playerAttackScript = null;

    public EnemyHealth enemyHealthScript = null;

    public float timer = 0f;

    // Use this for initialization
    void Start () {
        playerScoreScript = GameObject.Find("Player").GetComponent<Player_Score>();
        playerAttackScript = GameObject.Find("Player").GetComponent<Player_Attack>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAnimState(int state)
    {
        if (enemyAnim.GetInteger("State") != state)
        {
            enemyAnim.SetInteger("State", state);
        }
    }

    public void DeleteEnemy()
    {
        
        Destroy(transform.parent.gameObject);
    }

    public void DisableCollider()
    {
        playerScoreScript.score += playerAttackScript.scoreMultiplier * enemyHealthScript.amountOfPointsWorth;
        collider.SetActive(false);
    }

    float randPitchRange = 0.2f;

    public void RobotDeath_SFX()
    {
        GameObject deathSFX_GO = (GameObject)Instantiate(robotDeathSFX, transform.position, Quaternion.identity);

        deathSFX_GO.GetComponent<AudioSource>().pitch += Random.Range(-randPitchRange, randPitchRange);
    }
}
