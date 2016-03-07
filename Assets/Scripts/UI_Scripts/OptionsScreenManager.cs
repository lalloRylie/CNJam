using UnityEngine;
using System.Collections;

public class OptionsScreenManager : MonoBehaviour {

    public GameObject optionsScreenPanel = null;
    public GameObject creditsScreenPanel = null;
    public GameObject controlsScreenPanel = null;

    public void OnBackButtonPressed()
    {
        optionsScreenPanel.SetActive(false);
    }

    public void OnControlsButtonPressed() {
        controlsScreenPanel.SetActive(true);
    }

    public void OnCreditsButtonPressed()
    {
        creditsScreenPanel.SetActive(true);
        StartCoroutine(creditsScreenPanel.GetComponent<CreditsAnimationEvents>().Play(creditsScreenPanel.GetComponent<Animation>(), "CreditsAnimation", false, () => Debug.Log("onComplete")));
    }

    public void OnMusicVolumeChanged(float value)
    {
        DataCore.VolumeData.musicVolume = value;
        AudioManager.instance.musicSource.volume = DataCore.VolumeData.musicVolume;
    }

    public void OnSoundFXVolumeChanged(float value)
    {
        DataCore.VolumeData.soundFXVoume = value;
        AudioManager.instance.soundFXVolume = DataCore.VolumeData.soundFXVoume;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
