﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text_ScoreMultiplier : MonoBehaviour {

    public Player_Attack playerAttackScript;

    Text scoreMultiplierText;
	// Use this for initialization
	void Start () {
	    scoreMultiplierText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (playerAttackScript.scoreMultiplier <= 0)
        //{
        //    scoreMultiplierText.text = "";
        //    return;
        //}
        scoreMultiplierText.text = "x" + playerAttackScript.scoreMultiplier;
	}
}
