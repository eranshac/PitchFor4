using UnityEngine;
using System.Collections;

public class PopSoundFX : MonoBehaviour {

	public AudioSource myAudioSource;
	public int numberOfExplosions;
	/*public PopSoundFX(int i){
		numberOfExplosions=i;
	}*/
	void Start () {
		StartCoroutine("PlaySoundFx");
		Invoke ("DestroySoundFx",4);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DestroySoundFx(){
		Destroy (gameObject);
	}

	IEnumerator PlaySoundFx(){

		for (int i=1; i<=numberOfExplosions; i++) {
			myAudioSource.Play ();

			yield return new WaitForSeconds(0.1f);

		}
	}
}
