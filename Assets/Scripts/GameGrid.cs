using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameGrid : MonoBehaviour {
	
	public int numberOfColoumns;
	private static int numberOfPipes=12, NumberOfRowes=7;
	public static Spwaner spwaner;
	private static int countCallForCheck=0;
	public static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];
	//private static int x,y;
	//	private static Vector4 color;
	private static int points = 0;
	private static Text textPoints;
	public static int destroyd = 0;
	private static int pointsTypeOfSequanceCoefficient;
	public static int pointsNumOfSequanceCoefficient;
	public PointsAnimation SequenceAnim ;
	public PointsAnimation pointsAnim;
	private SequenceSound sequenceSound;
	private Queue < int > queueAnimations = new Queue < int >();

	int numOfSequensesForAnimation;
	int score;

	
	void Start () {
		sequenceSound = Resources.Load<SequenceSound> ("prefabs/SoundPrefabs/sequenceSound");
		textPoints= GameObject.Find("PointsPanel").transform.GetChild(0).gameObject.GetComponent<Text>();
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
		
	}

	void Update(){
		textPoints.text= " Score: "+ points.ToString();
		if (destroyd > 0) {
			print ("destroyd " + destroyd);
			print ("pointsCoefficient " + pointsTypeOfSequanceCoefficient);
			print ("pointsNumOfSequanceCoefficient " + pointsNumOfSequanceCoefficient); 
			int point;
			if (destroyd > 7) {

				point= pointsTypeOfSequanceCoefficient * 100 * ((int)Mathf.Pow (2, pointsNumOfSequanceCoefficient));
				destroyd=destroyd-4;
			} else{
				point= (destroyd - 3) * pointsTypeOfSequanceCoefficient * 100 * ((int)Mathf.Pow (2, pointsNumOfSequanceCoefficient));
				destroyd = 0;
		}
				CallAnimations(point,pointsNumOfSequanceCoefficient);
		AddPoints (point);
		pointsNumOfSequanceCoefficient++;

		}
		
	}

	void CallAnimations (int point, int pointsNumOfSequanceCoefficient)
	{
		if(pointsNumOfSequanceCoefficient>0)
		queueAnimations.Enqueue (pointsNumOfSequanceCoefficient);
		score=point;

		if(pointsNumOfSequanceCoefficient==1){
			Invoke("ActivateSequencesAnimation",0.1f);
		}
		if(pointsNumOfSequanceCoefficient==2){
			Invoke("ActivateSequencesAnimation",1.2f);
		}
		if(pointsNumOfSequanceCoefficient==3){
			Invoke("ActivateSequencesAnimation",2f);
		}
		if(pointsNumOfSequanceCoefficient==4){
			Invoke("ActivateSequencesAnimation",3f);
		}
		if(pointsNumOfSequanceCoefficient==5){
			Invoke("ActivateSequencesAnimation",4f);
		}
		ActivatePointsAnimation ();
		ActivatePointsSound ();
	}
	
	void ActivateSequencesAnimation ()
	{

		numOfSequensesForAnimation = queueAnimations.Dequeue ();
		print ("numOfSequensesForAnimation " + numOfSequensesForAnimation);
		if (numOfSequensesForAnimation != 0) {
			PointsAnimation sequencesAnimation = Resources.Load<PointsAnimation> ("prefabs/AnimationPrefabs/SequencesAnimation");
			string textToDisplay="TextNotInitialized";
			if(numOfSequensesForAnimation==1){
				textToDisplay="Double Explosion!";
			}
			if(numOfSequensesForAnimation==2){
				textToDisplay="Triple Explosion!";
			}
			if(numOfSequensesForAnimation==3){
				textToDisplay="Quadruple Explosion!";


			}
			if(numOfSequensesForAnimation==4){
				textToDisplay="Penta Explosion!";

			}
			sequencesAnimation.PointsTextController.text = textToDisplay ;
			SequenceAnim = (PointsAnimation)Instantiate (sequencesAnimation, new Vector3 (0, 0, 0), Quaternion.identity);
			GameObject animationCanvas = GameObject.FindGameObjectWithTag ("AnimationCanvas");
			SequenceAnim.transform.parent = animationCanvas.transform;
			Instantiate (sequenceSound);


			Invoke("destroyAnimation",3f);
		}
	}
	
	public  void ActivatePointsAnimation ()
	{

		PointsAnimation pointsAnimation=Resources.Load<PointsAnimation>("prefabs/AnimationPrefabs/PointsAnimation");
	

		pointsAnimation.PointsTextController.text = "+"+ score.ToString ();

		pointsAnim=(PointsAnimation)Instantiate(pointsAnimation,new Vector3(0,0,0),Quaternion.identity);
		GameObject animationCanvas =GameObject.FindGameObjectWithTag ("AnimationCanvas");
		pointsAnim.transform.parent = animationCanvas.transform;
	}
	public  void ActivatePointsSound ()
	{
		SuctionSoundFX pointsSound=Resources.Load<SuctionSoundFX>("prefabs/SoundPrefabs/PointsSound");
		Instantiate (pointsSound);

		//PointsSound

	}


	public void destroyAnimation(){
		Destroy (SequenceAnim);
		
	}
	
	
	

	
	public static void AddPoints (int pointsToAdd){
		points=points+pointsToAdd;
	}
	
	
	
	
	private static void CheckForSequence (int x, int y,Vector4 color){
		
		CheckForColumn(x,y,color);
		CheckForRow(x,y,color);
		CheckForDiagBottomLeft(x,y,color);
		CheckForDiagBottomRight(x,y,color);
		
		
		countCallForCheck--;
		
	}
	
	public static void InsertBallToGrid(Ball ball){
		Vector2 ballPosition= GetCurrentBallPosition(ball);
		insertBallIntoGrid (ballPosition,ball);
		
		CanCheck((int) GetCurrentBallPosition(ball).x,(int) GetCurrentBallPosition(ball).y,ball.GetBallColor());
		
		
		
		
	}
	
	private static void CanCheck(int x, int y, Vector4 color){
		if(countCallForCheck>0){
			
			CanCheck(x,y,color);
		}else{
			
			countCallForCheck++;
			CheckForSequence(x,y,color);
		}
	}
	public static Vector2 GetCurrentBallPosition (Ball ball)
	{
		float ballRadius = ball.GetComponent<CircleCollider2D>().radius;
		float ballXPosition = ball.transform.position.x;
		float GridX = Mathf.Round( (ballXPosition+1)/2 );
		float GridY = Mathf.Round(( ball.transform.position.y )/(ballRadius*2));
		Vector2 ballPosition = new Vector2 ((int)GridX-1,(int)GridY-1);
		return ballPosition;
	}
	
	public static void insertBallIntoGrid (Vector2 ballPosition,Ball ball)
	{
		grid[(int)ballPosition.x, (int)ballPosition.y] = ball;
	}
	
	
	
	public Spwaner GetSpwaner(){
		
		return spwaner;
	}
	
	private static void CheckForColumn(int x,int y, Vector4 color ){
		int up=CheckUp(x,y,color);
		int down= 	CheckDown(x,y,color);
		
		if(down+up >=4){
			pointsTypeOfSequanceCoefficient=1;
			MakePopSound(down+up);
			
			for (int i = 0; i < down; i++)
			{
				
				DestroyBallInGrid(x,y-i);
			}
			
			for (int i = 1; i <= up; i++)
			{
				
				DestroyBallInGrid(x,y+i);

			}

		}	
		
	}

	static void InstanciatExplotionAnimation (int x, int y)
	{

		if (grid [x, y].GetBallColor().x ==1 &&  grid [x, y].GetBallColor().y == 0.92f) {
			BallExplosion yellowExplotionAnimation=Resources.Load<BallExplosion>("prefabs/AnimationPrefabs/YellowBallExplosionPosition/YellowBallExplosionPosition");
			BallExplosion anim=	(BallExplosion)Instantiate(yellowExplotionAnimation,new Vector3(x,y,0),Quaternion.identity);	
			anim.transform.position = grid [x, y].transform.position;

		}
		if (grid [x, y].GetBallColor().x ==1 &&  grid [x, y].GetBallColor().y == 0 &&  grid [x, y].GetBallColor().z == 0) {
			BallExplosion redExplotionAnimation=Resources.Load<BallExplosion>("prefabs/AnimationPrefabs/RedBallExplosionPosition/RedBallExplosionPosition");
			BallExplosion anim=	(BallExplosion)Instantiate(redExplotionAnimation,new Vector3(x,y,0),Quaternion.identity);	
			anim.transform.position = grid [x, y].transform.position;
		}
		if (grid [x, y].GetBallColor().x ==0 &&  grid [x, y].GetBallColor().y == 1 &&  grid [x, y].GetBallColor().z == 0) {
			BallExplosion greenExplotionAnimation=Resources.Load<BallExplosion>("prefabs/AnimationPrefabs/GreenBallExplosionPosition/GreenBallExplosionPosition");
			BallExplosion anim=	(BallExplosion)Instantiate(greenExplotionAnimation,new Vector3(x,y,0),Quaternion.identity);	
			anim.transform.position = grid [x, y].transform.position;
		}
		if (grid [x, y].GetBallColor().x ==0.5f) {
			BallExplosion greyExplotionAnimation=Resources.Load<BallExplosion>("prefabs/AnimationPrefabs/GreyBallExplosionPosition/GreyBallExplosionPosition");
			BallExplosion anim=	(BallExplosion)Instantiate(greyExplotionAnimation,new Vector3(x,y,0),Quaternion.identity);	
			anim.transform.position = grid [x, y].transform.position;
		}
		if (grid [x, y].GetBallColor().x ==1 &&  grid [x, y].GetBallColor().y == 0 &&  grid [x, y].GetBallColor().z == 1 ) {
			BallExplosion purpleExplotionAnimation=Resources.Load<BallExplosion>("prefabs/AnimationPrefabs/PurpleBallExplosionPosition/PurpleBallExplosionPosition");
			BallExplosion anim=	(BallExplosion)Instantiate(purpleExplotionAnimation,new Vector3(x,y,0),Quaternion.identity);	
			anim.transform.position = grid [x, y].transform.position;
		}




	}










	
	private static int CheckDown(int x, int y, Vector4 color){
		
		int count =1;
		
		while (y-count>=0 && grid[x,y-count]&& grid[x,y-count].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	private static int CheckUp(int x, int y, Vector4 color){
		
		int count =0;
		
		while (y+count+1<NumberOfRowes && grid[x,y+1+count]	 && grid[x,y+1+count].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	
	private static void CheckForRow(int x, int y, Vector4 color){
		int MatchOnRight= CountRight(x,y,color);
		int MatchOnLeft = CountLeft (x,y,color);
		if(MatchOnLeft+MatchOnRight>=3){
			
			pointsTypeOfSequanceCoefficient=2;
			MakePopSound(MatchOnLeft+MatchOnRight+1);
			for (int i = 0; i <= MatchOnRight; i++)
			{	
				
				
				DestroyBallInGrid(x+i,y);
	
			}
			for (int i = 0; i < MatchOnLeft; i++)
			{
				
				DestroyBallInGrid(x-i-1,y);
			}
			
		}
	}
	
	
	
	static void MakePopSound (int numberOfExplosions)
	{
		PopSoundFX popSound=Resources.Load<PopSoundFX>("prefabs/SoundPrefabs/PopSound");
		popSound.numberOfExplosions = numberOfExplosions;
		Instantiate(popSound,new Vector3(0,0,0),Quaternion.identity);
	}
	
	private static void CheckForDiagBottomLeft(int x, int y, Vector4 color){
		int downLeft= CountDiagDownLeft(x,y,color);
		int upRight = CountDiagUpRight(x,y,color);
		if (downLeft+upRight>=3){
			
			pointsTypeOfSequanceCoefficient=4;
			MakePopSound(downLeft+upRight+1);
			for(int i=0;i<=upRight;i++){
				DestroyBallInGrid(x+i,y+i);

			}
			
			for(int i=1;i<=downLeft;i++){
				
				DestroyBallInGrid(x-i,y-i);

			}
		}
		
	}
	
	
	private static void	CheckForDiagBottomRight(int x, int y, Vector4 color){
		
		int downRight= CountDiagDownRight(x,y,color);
		int upLeft = CountDiagUpLeft(x,y,color);
		
		if (downRight+upLeft>=3){
			
			pointsTypeOfSequanceCoefficient=4;
			MakePopSound(downRight+upLeft+1);
			
			for(int i=0;i<=downRight;i++){
				
				DestroyBallInGrid(x+i,y-i);
			}
			
			for(int i=1;i<=upLeft;i++){
				
				DestroyBallInGrid(x-i,y+i);
			}
		}
		
		
	}
	
	
	private static int CountDiagUpLeft(int x, int y, Vector4 color){
		int count=0;
		while(x-count-1>=0 && y+count+1<NumberOfRowes && grid[x-count-1,y+count+1] && grid[x-count-1,y+count+1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	
	private static int CountDiagDownRight(int x, int y, Vector4 color){
		int count=0;
		while(x+count+1<numberOfPipes && y-count-1>=0 && grid[x+count+1,y-count-1] && grid[x+count+1,y-count-1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	private static int CountDiagDownLeft(int x, int y, Vector4 color){
		int count=0;
		while(x-count-1>=0 && y-count-1>=0 && grid[x-count-1,y-count-1] && grid[x-count-1,y-count-1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	private static int CountDiagUpRight(int x, int y, Vector4 color){
		int count=0;
		while(x+count+1<numberOfPipes && y+count+1<NumberOfRowes && grid[x+count+1,y+count+1] && grid[x+count+1,y+count+1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	//
	
	
	private static int CountRight(int x, int y, Vector4 color){
		int count =0;
		
		while(x+count+1< numberOfPipes && grid[x+1+count ,y] && grid[x+1+count ,y].GetBallColor()==color){
			
			count++;
		}
		
		return count;
	}
	
	
	public static void SetNullToPreviousPositionOfBall(int xPosition,int yPosition){
		grid [xPosition,yPosition] = null;
	}
	
	
	private static int CountLeft(int x, int y, Vector4 color){
		int count =0;
		
		while(x-count-1>=0 && grid[x-1-count ,y] && grid[x-1-count ,y].GetBallColor()==color){
			count++;
		}
		
		return count;
	}
	
	
	public static void DestroyBallInGrid (int x, int y)
	{
		InstanciatExplotionAnimation(x,y);
		Destroy(grid[x,y].gameObject);

		
	}
	public static int GetNumberOfRows(){
		
		return NumberOfRowes;
	}
	public static int GetnumberOfPipes(){
		
		return numberOfPipes;
	}
}