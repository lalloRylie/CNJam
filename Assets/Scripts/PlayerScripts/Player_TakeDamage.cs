using UnityEngine;
using System.Collections;

public class Player_TakeDamage : MonoBehaviour
{

    public int playerHealth = 3;
    public bool playerDead = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0) {
            playerDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        //GetComponent<Player_Attack>().BoardWipeEMP();
    }
}
