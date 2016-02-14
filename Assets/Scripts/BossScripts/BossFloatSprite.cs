using UnityEngine;
using System.Collections;

public class BossFloatSprite : MonoBehaviour
{
    private float floatTimer = 0f;
    private float floatSpeed = 1f;
    private float floatScale = 2f;
    private float floatDisplacementAmt = 1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        floatTimer += 2f * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, (Mathf.Sin(floatTimer * floatScale) * floatSpeed) + floatDisplacementAmt, transform.position.z);
    }
}
