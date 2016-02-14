using UnityEngine;
using System.Collections;

public class RunBackgroundImagePositioning : MonoBehaviour {

    public GameObject skySpriteGO;
    public GameObject background1GO;
    public GameObject background2GO;

    public GameObject camera;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        skySpriteGO.transform.position = new Vector3(camera.transform.position.x, skySpriteGO.transform.position.y, skySpriteGO.transform.position.z);
	}
}
