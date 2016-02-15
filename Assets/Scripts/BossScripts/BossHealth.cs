using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    [HideInInspector]
    public int health = 7;
    public int startHealth = 7;

	// Use this for initialization
	void Start () {
        health = startHealth;
	}

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("WinScreen");
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            //GetComponent<BossBehavior>().bossState = 3;
            Debug.Log("boss died");
            Destroy(gameObject);
            StartCoroutine(GoToMenu());
        }
	}
}
