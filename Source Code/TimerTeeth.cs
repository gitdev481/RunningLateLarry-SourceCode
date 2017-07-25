using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerTeeth : MonoBehaviour {
	
	
	public float  timer ;
	
	public float timepassed;
	
	public Text TimerFormat;
	
	public bool startTimer = false;
	
	public bool GameOver = false;

	public bool GameWin = false;
	
	private const int MaxSceneLoaded = 10; //total amount of scenes in the game
	
	//private ArrayList scenesLoaded = new ArrayList();

	public float winTimer = 0.0f;
	
	public float introTimer = 0.0f;
	
	public GameObject introPanel;

	bool introOver = false;


	
	// Use this for initialization
	void Start () {
		
		startTimer = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (introOver == false) {
			introTimer += Time.deltaTime;
		}
		
		if (introTimer >= 0.7f) {
			introPanel.SetActive(false);
			introOver = true;
		}
		
		if (startTimer == true) {
			if(GameWin == false){
				timer = 5.0f;
				timepassed += Time.deltaTime;
				System.TimeSpan t = (System.TimeSpan.FromSeconds (timer)) - (System.TimeSpan.FromSeconds (timepassed));
				TimerFormat.text = string.Format ("{0:D2}:{1:D2}", t.Seconds, t.Milliseconds);
				
				if (t.Seconds <= 0 && t.Milliseconds < 0 )
				{
					TimerFormat.text = "0:00";
					GameOver = true;
					
				}
			}
		}

		if (GameWin == true) {
			winTimer+= Time.deltaTime;
		}
		
		//if the level has been completed
		if(winTimer >=1.5f){
			//load the next level.
			//LoadMainMenu();
			Application.LoadLevel("CoffeeScene");
		}
		
		if (GameOver == true) {
			Application.LoadLevel("GameOverScene");
			//Debug.Log("PorcaMadonna");
			//LoadAnotherLevel();
		}
	}
	

	
	void LoadMainMenu()
	{
		Debug.Log ("LoadMainMenu");
		Application.LoadLevel(0);
	}
}