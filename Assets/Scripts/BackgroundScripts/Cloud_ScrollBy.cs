using UnityEngine;
using System.Collections;

public class Cloud_ScrollBy : MonoBehaviour {

    public float speed = -0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(speed, 0f, 0f) * Time.deltaTime);
	}
}
