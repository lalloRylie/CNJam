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
        counter += 10 * Time.deltaTime;
        Vector3 newScaleVector = new Vector3(transform.localScale.x + (1.5f * counter), transform.localScale.y + counter, 1f);


        if (counter > 2.5f)
            newScaleVector = new Vector3(transform.localScale.x / (counter * 1.5f), transform.localScale.y / (counter * 1.5f), 1f);

        transform.localScale = newScaleVector;

        if (counter > 10f)
            Destroy(gameObject);
    }
}
