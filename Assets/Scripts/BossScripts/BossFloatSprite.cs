using UnityEngine;
using System.Collections;

public class BossFloatSprite : MonoBehaviour
{
    private float floatTimer = 0f;
    public float floatSpeed = 1f;
    public float floatScale = 2f;
    public float floatDisplacementAmt = 1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.timeScale <= 0f) {
            return;
        }

        floatTimer += 2f * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(floatTimer * floatScale) * floatSpeed), transform.position.z);
    }
}
