using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GLOBALTIMER : MonoBehaviour {

	public bool timerEnabled = false;
	public float globalTimer = 0.0f;
	public AudioSource globalSound;
	public GameObject stopper;
	public GameObject winningTime;
	// bool stopGlobalMusic = false;

    public Text nameInputField;
    public Text passwordInputField;
    public string leaderboardName;
    public string leaderboardPassword;
    public float leaderboardScore;
    public InputField actualNameInputField;
    public InputField actualPasswordInputfield;

    public void SubmitLeaderboardScore()
    {

        leaderboardName = nameInputField.text;
        leaderboardScore = globalTimer;
        leaderboardPassword = passwordInputField.text;
        Debug.Log(leaderboardName);
        Debug.Log(leaderboardScore);
    }

    void Awake() {
		DontDestroyOnLoad(transform.gameObject);

        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        if (globalSound != null)
        {
            globalSound.Play();
        }
	}
	void Start(){
		timerEnabled = true;
	}
	void Update(){
	
	   if(Input.GetKeyDown(KeyCode.Escape))
	   {
	      Application.Quit();
 	   }



		if (timerEnabled == true) {
			if(Application.loadedLevel !=6){
				globalTimer+=Time.deltaTime;
				//globalTimer
			}
		}
		if (Application.loadedLevel == 6) {
			winningTime = GameObject.FindWithTag("WINTIME");
			winningTime.GetComponent<Text>().text = ("You got to work in " + globalTimer.ToString("F2") + " seconds");
            nameInputField = GameObject.FindWithTag("INPUTTEXT").GetComponent<Text>();
            passwordInputField = GameObject.FindWithTag("PASSWORDINPUT").GetComponent<Text>();
            actualNameInputField = GameObject.FindWithTag("NAME").GetComponent<InputField>();
            actualPasswordInputfield = GameObject.FindWithTag("PASSWORD").GetComponent<InputField>();

        

		}


		//if the level loaded is the key turn screen.
		if (Application.loadedLevel == 5) {
			stopper = GameObject.FindWithTag("TIMER");
			if(stopper.GetComponent<TimerCoffee>().GameWin == true){
				//if(stopGlobalMusic){
				globalSound.Stop ();
			}
		}

		//if the level loaded is the game over screen...
		if (Application.loadedLevel == 7) {
			globalSound.Stop ();
		}

	}



}
