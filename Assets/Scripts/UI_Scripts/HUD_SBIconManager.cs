using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_SBIconManager : MonoBehaviour
{

    Image icon;
    float timer;
    float alpha = 1f;
    int direction = 1;

    public float blinkSpeed = 0.5f;

    Player_Attack playerAttackScript;

    // Use this for initialization
    void Start()
    {
        icon = GetComponent<Image>();
        playerAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAttackScript.playerCharge > 15 && !playerAttackScript.halfBoardWipeUsed)
        {
            alpha += direction * blinkSpeed * Time.deltaTime;
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alpha);

            if (alpha < 0.4f)
            {
                direction = 1;
            }
            else if (alpha > 1f)
            {
                direction = -1;
            }
        }
        else
        {
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
        }
        

        
    }
}
