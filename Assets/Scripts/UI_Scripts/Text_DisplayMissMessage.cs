using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_DisplayMissMessage : MonoBehaviour {

    Image imageComp;
    Player_Attack playerAttackScript;

    float alpha = 0f;

	// Use this for initialization
	void Start () {
        imageComp = GetComponent<Image>();
        playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();

        playerAttackScript.OnPlayerMissedEvent += playerAttackScript_OnPlayerMissedEvent;

        imageComp.color = new Color(imageComp.color.r, imageComp.color.g, imageComp.color.b, alpha);
	}

    void playerAttackScript_OnPlayerMissedEvent()
    {
        alpha = 1f;
    }
	
	// Update is called once per frame
	void Update () {
	    if(alpha >= 0f) {
            alpha -= 1f * Time.deltaTime;
            imageComp.color = new Color(imageComp.color.r, imageComp.color.g, imageComp.color.b, alpha);
        }
	}
}
