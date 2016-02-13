using UnityEngine;
using System.Collections;

public class Player_FloatSprite : MonoBehaviour {

    float timer = 0f;

    public float scale = 2f;
    public float speed = 1f;

    public float posOffset = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += 2f * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, (Mathf.Sin(timer * scale) * speed) + posOffset, transform.position.z);
	}
}
