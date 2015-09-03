using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBall : MonoBehaviour {
	private bool isLanded=false;

	public Rigidbody2D rigidbody2d;
	float speed=5;
	private Text BallPitchDisplayToScreen;
	void Start () {
	}
	
	void Update () {
			float currentPitch=Controller.x ;
			speed=0;
		//	print (Controller.x);
			if (currentPitch> 0) {

				speed = (currentPitch - 220) / 15;
				
				
			}
			
			transform.position += Vector3.left * speed * Time.deltaTime;
			
	}
		


	

}
