using UnityEngine;
using System.Collections;

public class PipeHeads : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i=0; i<GameGrid.GetnumberOfPipes();i++){
			Transform child= this.gameObject.transform.GetChild(i);
			Animator animator = child.gameObject.GetComponent<Animator>();
			animator.SetBool("isWhite",true);
			child.GetComponent<SpriteRenderer>().sortingOrder=1;
		}
	}

	
	// Update is called once per frame
	void Update () {

		for(int i=0; i<GameGrid.GetnumberOfPipes();i++){
			Transform child= this.gameObject.transform.GetChild(i);
			Animator animator = child.gameObject.GetComponent<Animator>();
			if(GameGrid.grid[i,5] && animator.GetBool("isWhite")){
			
				animator.SetBool("isRed",true);
				animator.SetBool("isBlinking",true);
				animator.SetBool("isWhite",false);
				child.GetComponent<SpriteRenderer>().sortingOrder=4;
					
			}else if(!GameGrid.grid[i,5] && animator.GetBool("isRed")){
				
				animator.SetBool("isRed",false);
				animator.SetBool("isBlinking",false);
				animator.SetBool("isWhite",true);
				child.GetComponent<SpriteRenderer>().sortingOrder=1;
						
				}					

		} 
	} 
}
