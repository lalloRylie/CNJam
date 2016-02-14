using UnityEngine;
using System.Collections;

public class LoseScreenManager : MonoBehaviour {

    public GameObject loadingGO = null;

    public void OnRetryButtonPressed()
    {
        loadingGO.SetActive(true);
        Application.LoadLevel("GameScene");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
