using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackIconManager : MonoBehaviour {

    Image icon;
    Player_Attack playerAttackScript;
    float chargeAmount;

    public Sprite dash;
    public Sprite punch;
    public Sprite zap;


	// Use this for initialization
	void Start () {
        icon = GetComponent<Image>();
        playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();
	}
	
	// Update is called once per frame
	void Update () {
        chargeAmount = playerAttackScript.playerCharge;

        if(chargeAmount >= 20 )
        {
            icon.sprite = zap;
        }
        else if (chargeAmount >= 10 && chargeAmount < 20)
        {
            icon.sprite = punch;
        }
        else if (chargeAmount >= 0 && chargeAmount < 10)
        {
            icon.sprite = dash;
        }
    }
}
