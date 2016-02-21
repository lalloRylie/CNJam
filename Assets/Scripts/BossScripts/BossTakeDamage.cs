using UnityEngine;
using System.Collections;

public class BossTakeDamage : MonoBehaviour {

    public GameObject playerHitEnemyParticlePrefab = null;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage)
    {
        GetComponent<BossHealth>().health -= damage;
        Instantiate(playerHitEnemyParticlePrefab, transform.position, Quaternion.identity);
    }
}
