﻿using UnityEngine;
using System.Collections;

public class FirstCutsceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.instance.PlaySong(AudioManager.instance.friendshipMusic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
