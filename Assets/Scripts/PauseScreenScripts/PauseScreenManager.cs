using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PauseScreenManager : MonoBehaviour {

    public GameObject pausePanel = null;
    public GameObject optionsPanel = null;
    public GameObject pauseButton = null;
    public Blur cameraBlur = null;

    public void OnPauseButtonPressed()
    {
        Time.timeScale = 0f;
        cameraBlur.enabled = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        AudioManager.instance.musicSource.volume = DataCore.VolumeData.musicVolume * 0.25f;
    }

    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1f;
        AudioManager.instance.musicSource.volume = DataCore.VolumeData.musicVolume;
        cameraBlur.enabled = false;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnRestartButtonPressed()
    {
        Time.timeScale = 1f;
        // to cause music to restart, stop and start the music
        AudioManager.instance.musicSource.volume = DataCore.VolumeData.musicVolume;
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.musicSource.Play();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnOptionsButtonPressed()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        AudioManager.instance.musicSource.volume = DataCore.VolumeData.musicVolume;
        Application.LoadLevel("TitleScreen");
    }

	// Use this for initialization
	void Start () {
        //GameObject.Find();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
