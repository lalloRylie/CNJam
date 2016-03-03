using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public GameObject loadingTextGO;
    public GameObject optionsPanelGO = null;

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlaySong(AudioManager.instance.titleMusic);
    }

    public void StartStoryMode()
    {
        Debug.Log("story mode started");
        loadingTextGO.SetActive(true);
        Application.LoadLevel("FirstCutScene");
    }

    public void StartArcadeMode()
    {
        loadingTextGO.SetActive(true);
        Application.LoadLevel("EndlessGameMode");
    }

    public void OnOptionsButtonPressed()
    {
        optionsPanelGO.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        //if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
        //    loadingTextGO.SetActive(true);
        //    Application.LoadLevel("FirstCutScene");

        //}
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR 
        //if (Input.touchCount > 0)
        //{
        //    loadingTextGO.SetActive(true);
        //    Application.LoadLevel("GameScene");
        //}
#endif

    }
}
