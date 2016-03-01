using UnityEngine;
using System.Collections;

public class LoseScreenManager : MonoBehaviour {

    public GameObject loadingGO = null;

    public void OnRetryButtonPressed()
    {
        loadingGO.SetActive(true);
        Application.LoadLevel(DataCore.lastGameModePlayedSceneName);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    IEnumerator PlayLoseMusic()
    {
        AudioManager.instance.PlaySong(AudioManager.instance.loseMusic);
        yield return new WaitForSeconds(6f);
        AudioManager.instance.PlaySong(AudioManager.instance.titleMusic);
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayLoseMusic());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
