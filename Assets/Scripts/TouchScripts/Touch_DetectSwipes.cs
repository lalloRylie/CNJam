using UnityEngine;
using System.Collections;

public class Touch_DetectSwipes : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public float minSwipeDistY = 2f;

    public float minSwipeDistX = 2f;

    private Vector2 startPos;

    void Update()
    {
       // #if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:

                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    if (swipeDistVertical > minSwipeDistY)
                    {

                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                        if (swipeValue > 0)
                        {
                            //up swipe
                            //Jump ();
                        }

                        else if (swipeValue < 0)
                        {
                            //down swipe
                            //Shrink ();
                        }

                    }

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)
                    {

                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue > 0)
                        {
                            //right swipe
                            //MoveRight ();
                        }

                        else if (swipeValue < 0)
                        {
                            //left swipe
                            //MoveLeft ();
                        }

                    }
                    break;
            }
        }
//#endif
    }

}
