using UnityEngine;
using System.Collections;

public class SetSoundFXVolumeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().volume = DataCore.VolumeData.soundFXVoume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
