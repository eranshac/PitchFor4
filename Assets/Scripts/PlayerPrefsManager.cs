using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
		
	const string Highest_Pitch_Key = "Highest Pitch";	
	const string Lowest_Pitch_Key = "Lowest Pitch";	
	
	
	
	// Use this for initialization
	
	public static void SetHighestPitch(float pitch){
	
		PlayerPrefs.SetFloat(Highest_Pitch_Key,pitch);
	}
	
	public static float GetHighestPitch(){
	
		return PlayerPrefs.GetFloat(Highest_Pitch_Key);
	
	}
	
	public static void SetLowhestPitch(float pitch){
		
		PlayerPrefs.SetFloat(Lowest_Pitch_Key,pitch);
	}
	
	public static float GetLowhestPitch(){
		
		return PlayerPrefs.GetFloat(Lowest_Pitch_Key);
		
	}
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
