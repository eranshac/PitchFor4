using UnityEngine;
using System.Collections;

public class FBHolder : MonoBehaviour {

	// Use this for initialization
	
	
	void Awake(){
	
	FB.Init(SetInit,OnHideUnity);

	}
	private void SetInit(){
		print ("FB Init Done");
		print("FB.UserId   " + FB.UserId);
	if(FB.IsLoggedIn)
		{
	
	
	}else{
	
			FBlogin();
		}
	
	}
	
	private void OnHideUnity(bool isGameShown){
	
	if (!isGameShown){
	
		Time.timeScale=0;
	
		}else{
		
			Time.timeScale=1;
		
		}
	
	}
	
	void FBlogin(){
	
		FB.Login("user_about_me,user_birthday",AuthCallback);
	
	}
	
	void AuthCallback(FBResult result){
	
		if(FB.IsLoggedIn){
			
			print ("FB Is logged In");
			print("FB.UserId   " + FB.UserId);
			
			
		
		}else{
		
		print ("login faild");
		}
	
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
