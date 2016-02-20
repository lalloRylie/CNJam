using UnityEngine;
using System.Collections;

public class BossAnimationControllerScript : MonoBehaviour {

    Animator bossAnim;

	// Use this for initialization
	void Start () {
	    bossAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetBossAnimState(int animState)
    {
        if(bossAnim.GetInteger("State") != animState) {
            bossAnim.SetInteger("State", animState);
        }
    }
}
