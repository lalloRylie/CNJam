using UnityEngine;
using System.Collections;

public class Player_TakeDamage : MonoBehaviour
{

    public int playerHealth = 3;
    public bool playerDead = false;
    GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0) {
            playerDead = true;
            gameManager.GetComponent<GameStateControl>().SetGameState(3);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if(playerHealth > 0) {
            GetComponentInChildren<Player_ControlAnimationState>().SetAnimState(5);
        }        
    }

    
}
