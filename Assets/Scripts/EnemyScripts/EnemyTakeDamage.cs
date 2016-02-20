using UnityEngine;
using System.Collections;

public class EnemyTakeDamage : MonoBehaviour
{

    Player_Attack playerAttackScript = null;
    

    // Use this for initialization
    void Start()
    {
        playerAttackScript = GameObject.Find("Player").GetComponent<Player_Attack>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        GetComponent<EnemyHealth>().health -= damage;

        if (playerAttackScript.playerCharge < playerAttackScript.maxCharge)
        {
            playerAttackScript.playerCharge++;
        }
    }

}
