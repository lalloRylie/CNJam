using UnityEngine;
using System.Collections;

public class tearTest : MonoBehaviour {

    public float fallSpeed = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0f, -fallSpeed * Time.deltaTime));
        if (transform.position.y < -5f)
            Destroy(gameObject);
	}
}
