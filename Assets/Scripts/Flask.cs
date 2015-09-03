using UnityEngine;
using System.Collections;

public class Flask : MonoBehaviour {
	public Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
		myRigidbody.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	void OnCollisionEnter2D(Collision2D collider){
		string layer = LayerMask.LayerToName (collider.gameObject.layer);
		if (layer=="Ball") {
			myRigidbody.gravityScale = 1.5f;
		}

	}
}
