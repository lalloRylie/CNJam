using UnityEngine;
using System.Collections;

public class Player_TakeDamage : MonoBehaviour
{

    public int playerHealth = 3;
    GameObject gameManager;

    public Sprite deadSprite = null;
    public SpriteRenderer spriteRenderer;
    public Animator playerAnim;

    [HideInInspector]
    public bool canTakeDamage = true;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0) {
            gameManager.GetComponent<GameStateControl>().SetGameState(3);
            GetComponent<Player_Attack>().SetPlayerState(3);
            spriteRenderer.sprite = deadSprite;
            playerAnim.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage) playerHealth -= damage;

        if(playerHealth > 0) {
            GetComponentInChildren<Player_ControlAnimationState>().SetAnimState(5);
        }        
    }

    
}
