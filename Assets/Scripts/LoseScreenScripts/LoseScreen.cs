using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour
{

    public bool render = false;
    private const int width = 800;
    private const int height = 500;
    private Rect terminalMainWnd = new Rect(Screen.width / 2 - (width / 2), Screen.height / 2 - (height / 2), width, height);

    public void OpenLoseScreenUI()
    {
        render = true;
    }

    public void CloseLoseScreenUI()
    {
        render = false;
    }

    public void OnGUI()
    {
        if (render)
            terminalMainWnd = GUI.Window(0, terminalMainWnd, WindowFunction, "Lose Screen");

    }

    void WindowFunction(int windowID)
    {
        if (GUI.Button(new Rect(50, 400, 100, 50), "Retry"))
        {
            CloseLoseScreenUI();
            Application.LoadLevel("GameScene");
        }

        if (GUI.Button(new Rect(650, 400, 100, 50), "Quit"))
        {
            CloseLoseScreenUI();
            Application.Quit();
        }
    }
}
