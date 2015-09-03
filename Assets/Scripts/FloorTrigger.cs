using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {

	public static bool isTouchEnabledFirstTime=false;
	public static bool isTouchEnabledallways=false;
	private static GameObject pauseScreen;
	void Start () {
		pauseScreen=GameObject.Find("PauseScreen");
		pauseScreen.SetActive (false);
		print(pauseScreen.gameObject);



	}
	
	// Update is called once per frame
	public void LoadLevel (string name) {
				print(pauseScreen.gameObject);

		if(!isTouchEnabledFirstTime){
			// load Pause Screen
			pauseScreen.SetActive (true);
			pauseScreen.gameObject.SetActive(true);
			Time.timeScale = 0;

			}
			else{
			//
			if(isTouchEnabledallways){
			Application.LoadLevel(name);
			}
		
		}
	}
	void OnTriggerEnter2D(Collider2D flask){
		print (flask.gameObject.tag);
		if (flask.gameObject.tag =="MoreInformationFlask" && Application.loadedLevelName == "MainMenu") {
			Application.LoadLevel("Options");
		} 
		if(flask.gameObject.tag =="PlayFlask"){
			Application.LoadLevel ("GAME");
		}
		if(flask.gameObject.tag =="BackButton" && Application.loadedLevelName == "Options" ){
			Application.LoadLevel ("MainMenu");
		}
		if(flask.gameObject.tag =="BackButton" && Application.loadedLevelName == "Settings" ){
			Application.LoadLevel ("Options");
		}
		if(flask.gameObject.tag =="SettingFlask" && Application.loadedLevelName == "Options" ){
			Application.LoadLevel ("Settings");
		}

	/*	if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}
		if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}
		if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}*/


	}
}
