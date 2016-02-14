using UnityEngine;
using System.Collections;

public class Player_ControlAnimationState : MonoBehaviour
{

    public Animator playerAnim;
    GameObject playerGO;
    Player_Attack playerAttackScript;

    public GameObject shockProjectile;
    public GameObject shockBlastProjectile;

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

    public void Shock()
    {
        GameObject projectile = GameObject.Instantiate<GameObject>(shockProjectile);

        if (playerAttackScript.halfBoardWipeSideOnLeft)
        {
            //shoot blast left
            
        }
        else
        {
            //shoot blast right
            
        }
    }

    public void ShockBlast()
    {
        GameObject projectile = GameObject.Instantiate<GameObject>(shockBlastProjectile);

        if (playerAttackScript.halfBoardWipeSideOnLeft)
        {
            //shoot blast left
            projectile.transform.position = new Vector3(transform.position.x - 2f, -1.1f, 0f);
        }
        else
        {
            //shoot blast right
            projectile.transform.position = new Vector3(transform.position.x + 2f, -1.1f, 0f);
        }
    }
}
