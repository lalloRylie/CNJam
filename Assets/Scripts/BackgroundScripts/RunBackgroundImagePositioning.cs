using UnityEngine;
using System.Collections;

public class RunBackgroundImagePositioning : MonoBehaviour
{

    public GameObject skySpriteGO;
    public GameObject background1GO;
    public GameObject background2GO;
    public GameObject background3GO;

    public GameObject cloud1 = null;
    public GameObject cloud2 = null;
    public GameObject cloud3 = null;
    public GameObject cloud4 = null;

    public GameObject camera;

    GameObject left, middle, right;
    GameObject tempLeft, tempMiddle, tempRight;

    GameObject playerGO;

    float timer = 0f;
    float delay = 1.5f;

    float cloudSpawnTimer = 0f;
    float cloudSpawnDelay = 10f;

    float checkMaxDistCloudTimer = 0f;
    float checkMaxDistCloudAmountOfWaitTime = 4f;

    float changeDistance = 170f;

    // Use this for initialization
    void Start()
    {
        left = background1GO;
        middle = background2GO;
        right = background3GO;

        tempLeft = left;
        tempMiddle = middle;
        tempRight = right;

        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    void InfiniteScrollingBackground()
    {
        timer -= 1f * Time.deltaTime;

        if (timer > 0f)
        {
            return;
        }

        // check distance from the middle background
        float dist = middle.transform.position.x - playerGO.transform.position.x;
        //Debug.Log(dist);

        if (dist >= changeDistance)
        {
            // moving to the left, move the right background to the far left, reset left middle and right vars
            // get the left position, move the right to the left of that

            float leftPos = left.transform.position.x;
            float newLeftTargetPos = leftPos - 208.66f;

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
        else if (dist <= -changeDistance)
        {
            // moving to the right, move the left background to the far right, reset left middle and right vars

            float rightPos = right.transform.position.x;
            float newRightTargetPos = rightPos + 208.66f;

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

    void RunCloudSpawner()
    {
        float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;

        cloudSpawnTimer += 1f * Time.deltaTime;

        if (cloudSpawnTimer >= cloudSpawnDelay)
        {
            cloudSpawnTimer = 0f;

            int randNum = Random.Range(1, 5);
            Vector3 newPos = new Vector3(playerGO.transform.position.x + (horzExtent + 2f), 5.56f, 0f);

            if (randNum == 1)
            {
                Instantiate(cloud1, newPos, Quaternion.identity);
            }
            else if (randNum == 2)
            {
                Instantiate(cloud2, newPos, Quaternion.identity);
            }
            else if (randNum == 3)
            {
                Instantiate(cloud3, newPos, Quaternion.identity);
            }
            else if (randNum == 4)
            {
                Instantiate(cloud4, newPos, Quaternion.identity);
            }


        }

        checkMaxDistCloudTimer += 1f * Time.deltaTime;

        if (checkMaxDistCloudTimer >= checkMaxDistCloudAmountOfWaitTime)
        {
            checkMaxDistCloudTimer = 0f;

            GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

            // for spawning clouds if you're traveling very far to the right
            foreach (GameObject cloud in clouds)
            {
                float dist = cloud.transform.position.x - playerGO.transform.position.x;

                if (dist >= 0f)
                {
                    break;
                }

                if (cloud == clouds[clouds.Length - 1])
                {
                    int randNum = Random.Range(1, 5);
                    Vector3 newPos = new Vector3(playerGO.transform.position.x + (horzExtent + 2f), 5.56f, 0f);

                    if (randNum == 1)
                    {
                        Instantiate(cloud1, newPos, Quaternion.identity);
                    }
                    else if (randNum == 2)
                    {
                        Instantiate(cloud2, newPos, Quaternion.identity);
                    }
                    else if (randNum == 3)
                    {
                        Instantiate(cloud3, newPos, Quaternion.identity);
                    }
                    else if (randNum == 4)
                    {
                        Instantiate(cloud4, newPos, Quaternion.identity);
                    }
                    break;
                }
            }

            // for spawning clouds if you're traveling very far to the left
            foreach (GameObject cloud in clouds)
            {
                float dist = cloud.transform.position.x - playerGO.transform.position.x;

                if (dist <= 0f)
                {
                    break;
                }

                if (cloud == clouds[clouds.Length - 1])
                {
                    int randNum = Random.Range(1, 5);
                    Vector3 newPos = new Vector3(playerGO.transform.position.x - (horzExtent + 2f), 5.56f, 0f);

                    if (randNum == 1)
                    {
                        Instantiate(cloud1, newPos, Quaternion.identity);
                    }
                    else if (randNum == 2)
                    {
                        Instantiate(cloud2, newPos, Quaternion.identity);
                    }
                    else if (randNum == 3)
                    {
                        Instantiate(cloud3, newPos, Quaternion.identity);
                    }
                    else if (randNum == 4)
                    {
                        Instantiate(cloud4, newPos, Quaternion.identity);
                    }
                    break;
                }
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
        // make sky follow camera position
        skySpriteGO.transform.position = new Vector3(camera.transform.position.x, skySpriteGO.transform.position.y, skySpriteGO.transform.position.z);

        InfiniteScrollingBackground();

        RunCloudSpawner();

    }
}
