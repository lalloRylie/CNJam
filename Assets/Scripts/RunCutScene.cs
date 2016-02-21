using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunCutScene : MonoBehaviour {

    public Sprite image1 = null;
    public Sprite image2 = null;
    public Sprite image3 = null;
    public Sprite image4 = null;
    public Sprite image5 = null;

    float timer = 0.2f;
    float delay = 0.2f;

    Image imageComp;

	// Use this for initialization
	void Start () {
	    imageComp = GetComponent<Image>();
        imageComp.sprite = image1;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= 1.0f * Time.deltaTime;

        if (timer > 0f) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(imageComp.sprite == image1) {
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
                Application.LoadLevel("GameScene");
            }
        }
	}
}
