using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutScene_TransitionToBoss : MonoBehaviour
{

    public bool render = false;
    private int width = 900;
    private int height = 600;
    private Rect terminalMainWnd = new Rect(0f, 0f, Screen.width, Screen.height);
    int cutSceneState = 0;

    public Texture scene1;
    public Texture scene2;
    public Texture scene3;

    public GameObject boss;

    float timer = 1f;
    float delay = 0.2f;

    public Button pauseButton = null;

    //Screen.width / 2 - (width / 2), Screen.height / 2 - (height / 2)

    void Start()
    {
        timer = delay;
    }

    void Update()
    {
        if(GetComponent<GameStateControl>().gameState != 1) return;

        timer -= 1f * Time.deltaTime;
        if (timer > 0f) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))  
        {
            cutSceneState++;
            timer = delay;
        }

    }

    public void StartCutScene()
    {
        render = true;
    }

    public void EndCutScene()
    {
        render = false;
    }

    public void OnGUI()
    {
        if (render)
            terminalMainWnd = GUI.Window(0, terminalMainWnd, WindowFunction, "");
    }

    void WindowFunction(int windowID)
    {
        switch (cutSceneState)
        {
            case 0:
                AudioManager.instance.PlaySong(AudioManager.instance.bossMusic);
                pauseButton.interactable = false;
                cutSceneState = 1;
                break;
            case 1:
                
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene1);
                break;
            case 2:
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene2);
                break;
            case 3:
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene3);
                break;
            case 4:
                GameObject.Instantiate(boss);
                GetComponent<GameStateControl>().SetGameState(2);
                EndCutScene();
                pauseButton.interactable = true;
                cutSceneState = 5;
                break;
        }

    }
}
