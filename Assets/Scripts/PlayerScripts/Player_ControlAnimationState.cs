using UnityEngine;
using System.Collections;

public class Player_ControlAnimationState : MonoBehaviour {

    public Animator playerAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAnimState(int state)
    {
        if(playerAnim.GetInteger("State") != state) {
            playerAnim.SetInteger("State", state);
        }
    }
}
