using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OkayGotIt(){
		FloorTrigger.isTouchEnabledFirstTime = true;
		Time.timeScale = 1;
		GameObject pauseMenu =GameObject.Find("PauseScreen") ;
		pauseMenu.gameObject.SetActive(false);

	}
	public void EnablePressing(){
	
		FloorTrigger.isTouchEnabledFirstTime = true;
		FloorTrigger.isTouchEnabledallways = true;
		Time.timeScale = 1;
		GameObject pauseMenu =GameObject.Find("PauseScreen") ;
		pauseMenu.gameObject.SetActive(false);


	}
}
