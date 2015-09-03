using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridLayot : MonoBehaviour {
	public  GridLayot gridLayot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i =0;i<12;i++){
		
			for (int j=0;j<7;j++)
			{
			
				if(GameGrid.grid[i,j]){
				
					this.gameObject.transform.GetChild(7*i+j).GetComponent<Text>().color=GameGrid.grid[i,j].GetBallColor();
					 
				}else{
				
					this.gameObject.transform.GetChild(7*i+j).GetComponent<Text>().color=Color.black;
				}
			
			}
		
		
		}
	}
}
