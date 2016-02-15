using UnityEngine;
using System.Collections;

public class CutScene_TransitionToBoss : MonoBehaviour {

    public bool render = false;
    private const int width = 900;
    private const int height = 600;
    private Rect terminalMainWnd = new Rect(0f, 0f, width, height);
    int cutSceneState = 0;

    public Texture scene1;
    public Texture scene2;
    public Texture scene3;

    public GameObject boss;

    //Screen.width / 2 - (width / 2), Screen.height / 2 - (height / 2)


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            cutSceneState++;
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
        switch(cutSceneState)
        {
            case 0:
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene1);
                break;
            case 1:
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene2);
                break;
            case 3:
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene3);
                break;
            case 4:
                GameObject.Instantiate<GameObject>(boss);
                GetComponent<GameStateControl>().SetGameState(2);
                break;
        }
        
        
    }
}
