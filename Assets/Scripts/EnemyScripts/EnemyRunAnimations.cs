using UnityEngine;
using System.Collections;

public class EnemyRunAnimations : MonoBehaviour {

    public Animator enemyAnim = null;
    public GameObject collider = null;

    // Use this for initialization
    void Start () {
	
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
        collider.SetActive(false);
    }
}
