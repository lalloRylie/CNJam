using UnityEngine;
using System.Collections;

public class EnemyCollisionFix : MonoBehaviour {

    void OnCollisionStay2D(Collision2D coll)
    {
        float offset = 0.2f;
        //If this enemy is to the left of Sparko
        if (transform.parent.position.x < coll.transform.position.x)
        {
            //coll.transform.position = transform.parent.position - (GetComponent<Collider2D>().bounds.extents + coll.gameObject.GetComponentInChildren<Collider2D>().bounds.extents + new Vector3(offset, 0f));
            Vector3 newPos = coll.transform.position - (GetComponent<Collider2D>().bounds.extents + coll.gameObject.GetComponentInChildren<Collider2D>().bounds.extents + new Vector3(offset, 0f));
            newPos.y = transform.parent.position.y;
            newPos.z = 0f;
            transform.parent.position = newPos;
        }
        else
        {
            //coll.transform.parent.position = transform.parent.position + (GetComponent<Collider2D>().bounds.extents + coll.gameObject.GetComponentInChildren<Collider2D>().bounds.extents + new Vector3(offset, 0f));
            Vector3 newPos = coll.transform.position + (GetComponent<Collider2D>().bounds.extents + coll.gameObject.GetComponentInChildren<Collider2D>().bounds.extents + new Vector3(offset, 0f));
            newPos.y = transform.parent.position.y;
            newPos.z = 0f;
            transform.parent.position = newPos;
        }
    }
        // Use this for initialization
        void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
