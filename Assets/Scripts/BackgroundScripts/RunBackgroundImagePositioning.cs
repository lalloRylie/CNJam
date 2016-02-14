using UnityEngine;
using System.Collections;

public class RunBackgroundImagePositioning : MonoBehaviour {

    public GameObject skySpriteGO;
    public GameObject background1GO;
    public GameObject background2GO;
    public GameObject background3GO;

    public GameObject camera;

    GameObject left, middle, right;
    GameObject tempLeft, tempMiddle, tempRight;

    GameObject playerGO;

    float timer = 0f;
    float delay = 1f;

	// Use this for initialization
	void Start () {
        left = background1GO;
        middle = background2GO;
        right = background3GO;

        tempLeft = left;
        tempMiddle = middle;
        tempRight = right;

        playerGO = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // make sky follow camera position
        skySpriteGO.transform.position = new Vector3(camera.transform.position.x, skySpriteGO.transform.position.y, skySpriteGO.transform.position.z);

        timer -= 1f * Time.deltaTime;

        if(timer > 0f) {
            return;
        }

        // check distance from the middle background
        float dist = middle.transform.position.x - playerGO.transform.position.x;
        //Debug.Log(dist);

        if(dist >= 17f) {
            // moving to the left, move the right background to the far left, reset left middle and right vars
            // get the left position, move the right to the left of that

            float leftPos = left.transform.position.x;
            float newLeftTargetPos = leftPos - 36f;

            right.transform.position = new Vector3(newLeftTargetPos, right.transform.position.y, right.transform.position.z);

            tempLeft = right;
            tempMiddle = left;
            tempRight = middle;

            left = tempLeft;
            middle = tempMiddle;
            right = tempRight;

            // add a delay to fix flickering back and forth
            timer = delay;
        }
        else if(dist <= -17f) {
            // moving to the right, move the left background to the far right, reset left middle and right vars

            float rightPos = right.transform.position.x;
            float newRightTargetPos = rightPos + 36f;

            left.transform.position = new Vector3(newRightTargetPos, left.transform.position.y, left.transform.position.z);

            tempLeft = middle;
            tempMiddle = right;
            tempRight = left;

            left = tempLeft;
            middle = tempMiddle;
            right = tempRight;

            // add a delay to fix flickering back and forth
            timer = delay;
        }

	}
}
