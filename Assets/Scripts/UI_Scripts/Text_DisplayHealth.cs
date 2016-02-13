using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_DisplayHealth : MonoBehaviour {

    Text healthText;
    GameObject playerGO;
    Player_TakeDamage playerTakeDamageScript;

	// Use this for initialization
	void Start () {
	    healthText = GetComponent<Text>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerTakeDamageScript = playerGO.GetComponent<Player_TakeDamage>();
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "Health: " + playerTakeDamageScript.playerHealth;
	}
}
