using UnityEngine;
using System.Collections;

public class Player_ControlAnimationState : MonoBehaviour
{

    public Animator playerAnim;
    GameObject playerGO;
    Player_Attack playerAttackScript;

    public GameObject zapProjectile;
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

    public void Zap()
    {
        GameObject projectile = GameObject.Instantiate<GameObject>(zapProjectile);

        if (playerAttackScript.lastAttackDirectionWasLeft)
        {
            //shoot blast left
            projectile.transform.position = new Vector3(transform.position.x - 3f, -1.5f, 0f);
        }
        else
        {
            //shoot blast right
            projectile.transform.position = new Vector3(transform.position.x + 3f, -1.5f, 0f);
        }
    }

    public void ShockBlast()
    {
        GameObject projectile = GameObject.Instantiate<GameObject>(shockBlastProjectile);

        if (playerAttackScript.halfBoardWipeSideOnLeft)
        {
            //shoot blast left
            projectile.transform.position = new Vector3(transform.position.x - 8f, -1f, 0f);
        }
        else
        {
            //shoot blast right
            projectile.transform.position = new Vector3(transform.position.x + 8f, -1f, 0f);
        }
    }
}
