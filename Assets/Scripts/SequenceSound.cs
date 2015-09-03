using UnityEngine;
using System.Collections;

public class SequenceSound : MonoBehaviour {

	public AudioSource myAudioSource;
	void Start () {
		myAudioSource.Play();
		Invoke ("SelfDestruct", myAudioSource.clip.length);
	}
	
	// Update is called once per frame
	void SelfDestruct () {
		Destroy (gameObject);
	}
}
