using UnityEngine;
using System.Collections;

public class Cloud_ScrollBy : MonoBehaviour {

    public float speed = -0.01f;
    public GameObject playerGO = null;

	// Use this for initialization
	void Start () {
        playerGO = GameObject.Find("Player");
        transform.SetParent(GameObject.Find("BackgroundManager").transform);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(speed, 0f, 0f) * Time.deltaTime);

        float dist = Mathf.Abs(playerGO.transform.position.x - transform.position.x);

        if(dist > 70f) {
            Destroy(gameObject);
        }
        //Debug.Log(dist);
	}
}
