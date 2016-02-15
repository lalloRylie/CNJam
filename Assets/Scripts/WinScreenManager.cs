using UnityEngine;
using System.Collections;

public class WinScreenManager : MonoBehaviour {

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("TitleScreen");
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(GoToMenu());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
