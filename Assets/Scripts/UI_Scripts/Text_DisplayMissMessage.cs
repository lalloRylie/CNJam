using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_DisplayMissMessage : MonoBehaviour {

    Text textComp;
    Player_Attack playerAttackScript;

    float alpha = 0f;

	// Use this for initialization
	void Start () {
	    textComp = GetComponent<Text>();
        playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();

        playerAttackScript.OnPlayerMissedEvent += playerAttackScript_OnPlayerMissedEvent;

        textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, alpha);
	}

    void playerAttackScript_OnPlayerMissedEvent()
    {
        alpha = 1f;
    }
	
	// Update is called once per frame
	void Update () {
	    if(alpha >= 0f) {
            alpha -= 1f * Time.deltaTime;
            textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, alpha);
        }
	}
}
