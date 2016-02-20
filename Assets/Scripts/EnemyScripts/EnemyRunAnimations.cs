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
        playerScoreScript.score += playerAttackScript.scoreMultiplier * enemyHealthScript.amountOfPointsWorth;
        Destroy(transform.parent.gameObject);
    }

    public void DisableCollider()
    {
        collider.SetActive(false);
    }

    public void RobotDeath_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject.Instantiate<GameObject>(robotDeathSFX);

        if (timer > 2f)
            Destroy(gameObject);
    }
}
