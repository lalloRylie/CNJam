using UnityEngine;
using System.Collections;

public class MI_SplashScreenManager : MonoBehaviour
{

    float timer = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += 1f * Time.deltaTime;

        if (timer > 1.5f)
        {
            timer = 0f;
            Application.LoadLevel("TitleScreen");
        }
    }
}
