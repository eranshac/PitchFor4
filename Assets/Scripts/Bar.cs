using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public float GetWidthOfBar(){
		return GetComponent<BoxCollider2D>().size.x;

	}

}
