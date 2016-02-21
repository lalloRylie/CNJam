using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_HealthBar : MonoBehaviour {

    GameObject player;
    Player_TakeDamage playerTakeDamageScript;

    public Sprite fullCharge;
    public Sprite halfCharge;
    public Sprite lowCharge;
    public Sprite deadCharge;

	// Use this for initialization
	void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
        playerTakeDamageScript = player.GetComponent<Player_TakeDamage>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (playerTakeDamageScript.playerHealth)
        {
            case 3:
                GetComponent<Image>().sprite = fullCharge;
                break;
            case 2:
                GetComponent<Image>().sprite = halfCharge;
                break;
            case 1:
                GetComponent<Image>().sprite = lowCharge;
                break;
            case 0:
                GetComponent<Image>().sprite = deadCharge;
                break;
            default:
                GetComponent<Image>().sprite = deadCharge;
                break;
        }
	}
}
