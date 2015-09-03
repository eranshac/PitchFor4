using UnityEngine;
using System.Collections;

public class BallExplosion : MonoBehaviour {

	public Animator myAnimator;
	public SpriteRenderer mySpriteRenderer;
	void Start () {
		myAnimator=transform.GetChild(0).gameObject.GetComponent<Animator>();
		mySpriteRenderer=transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		Invoke ("SelfDestruuct",2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SelfDestruuct(){
		Destroy (gameObject);
	}
}
