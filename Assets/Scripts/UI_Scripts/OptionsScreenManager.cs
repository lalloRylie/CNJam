using UnityEngine;
using System.Collections;

public class OptionsScreenManager : MonoBehaviour {

    public GameObject optionsScreenPanel = null;
    public GameObject creditsScreenPanel = null;

    public void OnBackButtonPressed()
    {
        optionsScreenPanel.SetActive(false);
    }

    public void OnCreditsButtonPressed()
    {
        optionsScreenPanel.SetActive(true);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
