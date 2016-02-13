using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //If health drops to zero, enter death state
        if (health <= 0) {
            GetComponent<EnemyBehavior>().enemyState = 4;
        }
	}
}
