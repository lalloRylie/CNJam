using UnityEngine;
using System.Collections;

public class DeleteAfterSeconds : MonoBehaviour {

    public float seconds = 4f;
    float timer = 0f;
	
	// Update is called once per frame
	void Update () {
        timer += 1f * Time.deltaTime;

        if(timer >= seconds) {
            timer = 0f;
            Destroy(gameObject);
        }
	}
}
