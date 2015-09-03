using UnityEngine;
using System.Collections;
using PitchDetector;

/*******************************************************
Very simple template for use pitch detector.
Mic is enabled in the Start() function
The Updte() funtion will detect the pitch and print out

Use this script as base of your development
If you wish to see a ore complex use of pitch detector
tak a look at the example folder
*******************************************************/
public class template : MonoBehaviour {

	public GUIText noteText;							//GUI txt where the note will be printed

	private Detector pitchDetector;						//Pitch detector object

	private int minFreq, maxFreq; 						//Max and min frequencies window
	public string selectedDevice { get; private set; }	//Mic selected
	private bool micSelected = false;					//Mic flag

	float[] data;										//Sound samples data
	public int cumulativeDetections= 5; 				//Number of consecutive detections used to determine current note
	int [] detectionsMade;								//Detections buffer
	private int maxDetectionsAllowed=50;				//Max buffer size
	private int detectionPointer=0;						//Current buffer pointer
	public int pitchTimeInterval=500; 					//Millisecons needed to detect tone
	private float refValue = 0.1f; 						// RMS value for 0 dB
	public float minVolumeDB=-17f;						//Min volume in bd needed to start detection
	
	private int currentDetectedNote =0;					//Las note detected (midi number)
	private string currentDetectedNoteName;				//Note name in modern notation (C=Do, D=Re, etc..)

	void Awake() {
		pitchDetector = new Detector();
		pitchDetector.setSampleRate(AudioSettings.outputSampleRate);
	}

	void Start () {
		selectedDevice = Microphone.devices[0].ToString();
		micSelected = true;
		GetMicCaps();
		
		//Estimates bufer len, based on pitchTimeInterval value
		int bufferLen = (int)Mathf.Round (AudioSettings.outputSampleRate * pitchTimeInterval / 1000f);
		Debug.Log ("Buffer len: " + bufferLen);
		data = new float[bufferLen];
		
		detectionsMade = new int[maxDetectionsAllowed]; //Allocates detection buffer

		setUptMic();
	}

	// This function will detect the pitch
	void Update () {
		audio.GetOutputData(data,0);
		float sum = 0f;
		for(int i=0; i<data.Length; i++)
			sum += data[i]*data[i];
		float rmsValue = Mathf.Sqrt(sum/data.Length);
		float dbValue = 20f*Mathf.Log10(rmsValue/refValue);
		if(dbValue<minVolumeDB) {
			//Sound too low
			noteText.text="Note: <<";
			return;
		}

		//PITCH DTECTED!!
		pitchDetector.DetectPitch (data);
		int midiant = pitchDetector.lastMidiNote ();
		int midi = findMode ();
		noteText.text="Note: "+pitchDetector.midiNoteToString(midi);
		detectionsMade [detectionPointer++] = midiant;
		detectionPointer %= cumulativeDetections;
	
	}

	void setUptMic() {
		audio.volume = 0f;
		audio.clip = null;
		audio.loop = true; // Set the AudioClip to loop
		audio.mute = false; // Mute the sound, we don't want the player to hear it
		StartMicrophone();
	}
	
	public void GetMicCaps () {
		Microphone.GetDeviceCaps(selectedDevice, out minFreq, out maxFreq);//Gets the frequency of the device
		if ((minFreq + maxFreq) == 0)
			maxFreq = 44100;
	}
	
	public void StartMicrophone () {
		audio.clip = Microphone.Start(selectedDevice, true, 10, maxFreq);//Starts recording
		while (!(Microphone.GetPosition(selectedDevice) > 0)){} // Wait until the recording has started
		audio.Play(); // Play the audio source!
	}
	
	public void StopMicrophone () {
		audio.Stop();//Stops the audio
		Microphone.End(selectedDevice);//Stops the recording of the device	
	}
	
	int repetitions(int element) {
		int rep = 0;
		int tester=detectionsMade [element];
		for (int i=0; i<cumulativeDetections; i++) {
			if(detectionsMade [i]==tester)
				rep++;
		}
		return rep;
	}
	
	public int findMode() {
		cumulativeDetections = (cumulativeDetections >= maxDetectionsAllowed) ? maxDetectionsAllowed : cumulativeDetections;
		int moda = 0;
		int veces = 0;
		for (int i=0; i<cumulativeDetections; i++) {
			if(repetitions(i)>veces)
				moda=detectionsMade [i];
		}
		return moda;
	}
}
