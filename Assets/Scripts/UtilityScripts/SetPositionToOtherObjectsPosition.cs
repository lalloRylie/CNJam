using UnityEngine;
using System.Collections;

public class SetPositionToOtherObjectsPosition : MonoBehaviour {

    public GameObject setObjectPosition = null;

	// Use this for initialization
	void Start () {
        transform.position = setObjectPosition.transform.position;
	}
}
