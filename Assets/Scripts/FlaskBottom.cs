using UnityEngine;
using System.Collections;

public class FlaskBottom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D flask){
		if (flask.gameObject.tag =="menuBall") {
			GameObject theFlaskObject=gameObject.transform.root.gameObject.transform.GetChild(0).gameObject;
			theFlaskObject.GetComponent<Animator>().enabled=false;
			Rigidbody2D rigidBodyOfRoot=gameObject.transform.root.gameObject.GetComponent<Rigidbody2D>();
			rigidBodyOfRoot.isKinematic=false;
			rigidBodyOfRoot.gravityScale=3;
			rigidBodyOfRoot.velocity=new Vector2(-4,0);
			
		} 
		if (flask.gameObject.tag == "menuBall") {
			ClinkSound clinkSound = Resources.Load<ClinkSound> ("prefabs/SoundPrefabs/ClinkSound");
			Instantiate(clinkSound);
		}
		
		
	}
}
