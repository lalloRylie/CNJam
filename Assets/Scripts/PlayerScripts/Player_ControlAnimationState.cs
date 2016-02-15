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

    public float punchTimer = 0f;
    public float shockBlastTimer = 0f;
    public float empTimer = 0f;
    public float dashTimer = 0f;
    public float zapTimer = 0f;

    bool timerBool1 = false;
    bool timerBool2 = false;
    bool timerBool3 = false;
    bool timerBool4 = false;
    bool timerBool5 = false;

    // Use this for initialization
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = playerGO.GetComponent<Player_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerBool1)
            punchTimer += 1f * Time.deltaTime;

        if (timerBool1)
            shockBlastTimer += 1f * Time.deltaTime;

        if (timerBool1)
            empTimer += 1f * Time.deltaTime;

        if (timerBool1)
            dashTimer += 1f * Time.deltaTime;

        if (timerBool1)
            zapTimer += 1f * Time.deltaTime;
    }

    void SwitchBool(bool timerBool) {
        timerBool = !timerBool;
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
        SwitchBool(timerBool1);
        punchTimer = 0f;

        GameObject tempGO = GameObject.Instantiate<GameObject>(punchSFX);

        if (punchTimer > 1f)
        {
            Destroy(tempGO);
            SwitchBool(timerBool1);
        }
    }

    public void EMP_SFX()
    {
        empTimer = 0f;

        GameObject tempGO = GameObject.Instantiate<GameObject>(empSFX);

        if (empTimer > 1.5f)
        {
            Destroy(tempGO);
            SwitchBool(timerBool2);
        }
    }

    public void ShockBlast_SFX()
    {
        shockBlastTimer = 0f;

        GameObject tempGO = GameObject.Instantiate<GameObject>(ShockBlastSFX);

        if (shockBlastTimer > 2.1f)
        {
            Destroy(tempGO);
            SwitchBool(timerBool3);
        }
    }

    public void Dash_SFX()
    {
        dashTimer = 0f;

        GameObject tempGO = GameObject.Instantiate<GameObject>(dashSFX);
        tempGO.GetComponent<AudioSource>().time = 0.2f;

        if (dashTimer > 1f)
        {
            Destroy(tempGO);
            SwitchBool(timerBool4);
        }
    }

    //public void Zap_SFX()
    //{
    //    zapTimer = 0f;

    //    GameObject tempGO = GameObject.Instantiate<GameObject>(zapSFX);

    //    if (zapTimer > 0.6f)
    //    {
    //        Destroy(tempGO);
    //        SwitchBool(timerBool5);
    //    }
    //}
}
