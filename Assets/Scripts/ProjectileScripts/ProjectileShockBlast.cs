using UnityEngine;
using System.Collections;

public class ProjectileShockBlast : MonoBehaviour {

    GameObject player;
    Player_Attack playerAttackScript;
    bool hasFlipped = false;
    bool checkedOrientation = false;
    bool isLeft = false;
    float counter = 0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<Player_Attack>();
        if (!playerAttackScript.halfBoardWipeSideOnLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        counter += 5f * Time.deltaTime;

       
	}
}
