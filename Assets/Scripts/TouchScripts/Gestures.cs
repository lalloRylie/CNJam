using UnityEngine;
using System.Collections;

public class Gestures : MonoBehaviour
{

    //NEED TO CALIBRATE
    public float minSwipeDist;
    private Vector2 startPos;

    // Use this for initialization
    void Start()
    {
        minSwipeDist = Screen.width / 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch playerTouch = Input.GetTouch(0);

            switch (playerTouch.phase)
            {
                case TouchPhase.Began:
                    startPos = playerTouch.position;

                    //TAP
                    if (startPos.x < Screen.width / 2)
                    {
                        //The player tapped left
                    }
                    else if (startPos.x > Screen.width / 2)
                    {
                        //The player tapped right
                    }
                    //END TAP

                    break;
                case TouchPhase.Moved:
                    //SWIPE
                    Vector2 swipeVector = playerTouch.position - startPos;
                    float swipeVectorMag = Mathf.Sqrt(swipeVector.x * swipeVector.x + swipeVector.y * swipeVector.y);

                    //if (Mathf.Abs(swipeVector.x) > minSwipeDist || Mathf.Abs(swipeVector.y) > minSwipeDist)
                    if (swipeVectorMag > minSwipeDist)
                    {
                        if (swipeVector.x < 0)
                        {
                            //The player swiped left
                        }
                        else
                        {
                            //The player swiped right
                        }
                    }
                    //END SWIPE

                    break;
                case TouchPhase.Ended:

                    break;
            }
        }
    }
}
