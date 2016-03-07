using UnityEngine;
using System.Collections;

public class SetPosToOtherObjectByName : MonoBehaviour {

    public string objectName = "";

	// Use this for initialization
	void Start () {
        transform.position = GameObject.Find(objectName).transform.position;
	}
	
}
