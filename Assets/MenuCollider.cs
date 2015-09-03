using UnityEngine;
using System.Collections;

public class MenuCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D ball){
		if (ball.gameObject.tag == "menuBall") {
			ClinkSound clinkSound = Resources.Load<ClinkSound> ("prefabs/SoundPrefabs/ClinkSound");
			Instantiate(clinkSound);
		}
	}
}
