using UnityEngine;
using System.Collections;

public class SetPosition : MonoBehaviour {

    public Vector3 position = new Vector3(0f,0f,0f);

	// Use this for initialization
	void Start () {
        transform.position = position;
	}
}
