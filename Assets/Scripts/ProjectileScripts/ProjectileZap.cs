using UnityEngine;
using System.Collections;

public class ProjectileZap : MonoBehaviour {

    GameObject player;
    Player_Attack playerAttackScript;
    float counter = 0f;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<Player_Attack>();
        if (!playerAttackScript.lastAttackDirectionWasLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        counter += 1f * Time.deltaTime;

        if (counter > 0.15f)
            Destroy(gameObject);
    }
}
