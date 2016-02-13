using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 lerpPos = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime* 2f);
        lerpPos.y = transform.position.y;
        lerpPos.z = transform.position.z;
        transform.position = lerpPos;
	}
}
