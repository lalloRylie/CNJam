using UnityEngine;
using System.Collections;

public class PlayCreditsScreenAnimation : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        StartCoroutine(GetComponent<CreditsAnimationEvents>().Play(GetComponent<Animation>(), "CreditsAnimation", false, () => Debug.Log("onComplete")));
        AudioManager.instance.PlaySong(AudioManager.instance.friendshipMusic);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
