using UnityEngine;
using System.Collections;

public class EnemyTakeDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage) {
        GetComponent<EnemyHealth>().health -= damage;
    }

}
