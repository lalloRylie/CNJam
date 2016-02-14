using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    public int health = 7;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            GetComponent<BossBehavior>().bossState = 3;
        }
	}
}
