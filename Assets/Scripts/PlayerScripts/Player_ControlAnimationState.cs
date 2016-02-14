using UnityEngine;
using System.Collections;

public class Player_ControlAnimationState : MonoBehaviour
{

    public Animator playerAnim;
    GameObject playerGO;
    Player_Attack playerAttackScript;

    // Use this for initialization
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = playerGO.GetComponent<Player_Attack>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // animation events
    public void SetAnimState(int state)
    {
        if (playerAnim.GetInteger("State") != state)
        {
            playerAnim.SetInteger("State", state);
        }
    }

    public void ApplyFullBoardWipeDamage()
    {
        playerAttackScript.BoardWipeEMP();
    }

    public void ApplyHalfBoardWipeDamage()
    {
        playerAttackScript.HalfBoardWipe(playerAttackScript.halfBoardWipeSideOnLeft);
    }
}
