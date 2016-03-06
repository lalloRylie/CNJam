using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

    [HideInInspector]
    public int health = 7;
    public int startHealth = 7;

    public bool isBossDead = false;

    public BossAnimationControllerScript bossAnimControllerScript = null;
    public BossBehavior bossBehaviorScript = null;

	// Use this for initialization
	void Start () {
        health = startHealth;
	}

	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            bossAnimControllerScript.SetBossAnimState(3);
           // Debug.Log("boss died");
            isBossDead = true;
            
            //Set game state to final cutscene
            
        }
	}
}
