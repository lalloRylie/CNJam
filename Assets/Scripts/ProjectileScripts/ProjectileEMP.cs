using UnityEngine;
using System.Collections;

public class ProjectileEMP : MonoBehaviour {

    float counter = 0f;

    // Use this for initialization
    void Start()
    {
        //modifies alpha of the EMP blast sprite
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        counter += 20f * Time.deltaTime;
        Vector3 newScaleVector = new Vector3(transform.localScale.x, transform.localScale.y / counter, 1f);
        transform.localScale = newScaleVector;

        if (counter > 10f)
            Destroy(gameObject);
    }
}
