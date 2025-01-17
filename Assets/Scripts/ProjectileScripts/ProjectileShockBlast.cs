﻿using UnityEngine;
using System.Collections;

public class ProjectileShockBlast : MonoBehaviour {

    GameObject player;
    Player_Attack playerAttackScript;
    float counter = 0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<Player_Attack>();
        if (!playerAttackScript.halfBoardWipeSideOnLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
        counter += 10f * Time.deltaTime;
        Vector3 newScaleVector = new Vector3(transform.localScale.x, transform.localScale.y / counter, 1f);
        transform.localScale = newScaleVector;

        if (counter > 10f)
            Destroy(gameObject);
	}
}
