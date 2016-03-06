using UnityEngine;
using System.Collections;

public class Player_TakeDamage : MonoBehaviour
{

    public int playerHealth = 3;
    GameObject gameManager;

    public Sprite deadSprite = null;
    public SpriteRenderer spriteRenderer;
    public Animator playerAnim;

    public Player_Attack playerAttackScript = null;

    public float playerTakeDamageDelayTimer = 0f;
    public float amountOfDelayAfterTakingDamage = 1f;

    [HideInInspector]
    public bool canTakeDamage = true;

    public delegate void PlayerTakenDamageDelegate();
    public event PlayerTakenDamageDelegate PlayerTakenDamageEvent = null;
    public void Trigger_PlayerTakenDamageEvent() { if (PlayerTakenDamageEvent != null) PlayerTakenDamageEvent(); }

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        playerTakeDamageDelayTimer -= 1f * Time.deltaTime;

        if (playerHealth <= 0) {
            gameManager.GetComponent<GameStateControl>().SetGameState(3);
            GetComponent<Player_Attack>().SetPlayerState(3);
            spriteRenderer.sprite = deadSprite;
            playerAnim.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerTakeDamageDelayTimer >= 0f) return;

        if (canTakeDamage)
        {
            playerHealth -= damage;
            Trigger_PlayerTakenDamageEvent();
            playerAttackScript.DeductScoreMultiplier(true);
            playerTakeDamageDelayTimer = amountOfDelayAfterTakingDamage;
        }

        if(playerHealth > 0) {
            // set the player to emp
            GetComponentInChildren<Player_ControlAnimationState>().SetAnimState(7);
        }        
    }

    
}
