using UnityEngine;
using System.Collections;

public class BossTakeDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void TakeDamage(int damage)
    {
        GetComponent<BossHealth>().health -= damage;
    }
}
