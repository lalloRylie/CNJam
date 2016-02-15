using UnityEngine;
using System.Collections;

public class Player_ControlAnimationState : MonoBehaviour
{

    public Animator playerAnim;
    GameObject playerGO;
    Player_Attack playerAttackScript;

    public GameObject zapProjectile;
    public GameObject shockBlastProjectile;


    public GameObject punchSFX;
    public GameObject ShockBlastSFX;
    public GameObject empSFX;
    public GameObject dashSFX;
    public GameObject zapSFX;

    public float timer = 0f;

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

    public void Punch_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject tempGO = GameObject.Instantiate<GameObject>(punchSFX);

        if (timer > 1f)
            Destroy(tempGO);
    }

    public void EMP_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject tempGO = GameObject.Instantiate<GameObject>(empSFX);

        if (timer > 1.5f)
            Destroy(tempGO);
    }

    public void ShockBlast_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject tempGO = GameObject.Instantiate<GameObject>(ShockBlastSFX);

        if (timer > 2.1f)
            Destroy(tempGO);
    }

    public void Dash_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject tempGO = GameObject.Instantiate<GameObject>(dashSFX);
        tempGO.GetComponent<AudioSource>().time = 0.2f;

        if (timer > 1f)
            Destroy(tempGO);
    }

    public void Zap_SFX()
    {
        timer += 1f * Time.deltaTime;

        GameObject tempGO = GameObject.Instantiate<GameObject>(zapSFX);

        if(tempGO.GetComponent<AudioSource>().time == 0.6f)
            tempGO.GetComponent<AudioSource>().Stop();

        if (timer > 0.6f)
            Destroy(tempGO);
    }
}
