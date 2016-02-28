using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public GameObject loadingTextGO;

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlaySong(AudioManager.instance.titleMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	    if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            loadingTextGO.SetActive(true);
            Application.LoadLevel("FirstCutScene");

        }
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR 
        if (Input.touchCount > 0)
        {
            loadingTextGO.SetActive(true);
            Application.LoadLevel("GameScene");
        }
#endif

    }
}
