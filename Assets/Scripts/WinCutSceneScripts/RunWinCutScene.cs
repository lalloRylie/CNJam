using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunWinCutScene : MonoBehaviour {

    public Sprite scene1 = null;
    public Sprite scene2 = null;
    public Sprite scene3 = null;
    public Sprite scene4 = null;
    public Sprite scene5 = null;

    float timer = 1f;
    float delay = 0.2f;

    Image imageRend;

    // Use this for initialization
    void Start()
    {
        imageRend = GetComponent<Image>();
        imageRend.sprite = scene1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1.0f * Time.deltaTime;

        if (timer > 0f) return;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (imageRend.sprite == scene1)
            {
                imageRend.sprite = scene2;
                timer = delay;
            }

            else if (imageRend.sprite == scene2)
            {
                imageRend.sprite = scene3;
                timer = delay;
            }

            else if (imageRend.sprite == scene3)
            {
                imageRend.sprite = scene4;
                timer = delay;
            }

            else if (imageRend.sprite == scene4)
            {
                imageRend.sprite = scene5;
                timer = delay;
            }

            else if (imageRend.sprite == scene5)
            {
                Application.LoadLevel("WinScreen");
            }
        }
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR 
        if (Input.touchCount > 0)
        {
            if (imageComp.sprite == image1)
            {
                imageComp.sprite = image2;
                timer = delay;
            }

            else if (imageComp.sprite == image2)
            {
                imageComp.sprite = image3;
                timer = delay;
            }

            else if (imageComp.sprite == image3)
            {
                imageComp.sprite = image4;
                timer = delay;
            }

            else if (imageComp.sprite == image4)
            {
                imageComp.sprite = image5;
                timer = delay;
            }

            else if (imageComp.sprite == image5)
            {
                Application.LoadLevel("WinScreen");
            }
        }

#endif
    }
}
