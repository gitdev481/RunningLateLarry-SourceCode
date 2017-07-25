using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGameButton();
        }
	}

	public void StartGameButton(){
		Application.LoadLevel ("alarmClockScene");
	
	}
    public void LeaderboardsButton()
    {
        Application.LoadLevel("leaderboardScene");
    }
}
