using UnityEngine;
using System.Collections;

public class BrushTeeth : MonoBehaviour {

	string prevKeyboardSetting = "Right";
	int count; //Count for how many times buttons have been pressed
	float tCount; //Count of real seconds
	int textCount;
	bool what, failed;
	float cCount;
	public GameObject text;
	public GameObject fired;

	public GameObject fail;
	public GameObject la, ra; //Left Arrow, Right arrow
	public GameObject laf, raf; //Left Arrow, Right arrow
	public GameObject tb; //Toothbrush
	public GameObject ct; //Cleen teeth
	Vector3 ty, tx, cy; //The amount to move the toothbrush by each key stroke
	public GameObject winPanel;
	public TimerTeeth timerTeeth;

	public AudioSource vibrate;
	public AudioSource gameWinSound;
    public GameObject winSound;
    public TimerTeeth teethTimer;

    public float overBrushTimer = 0f;
    public float overBrushTimerThreshold = 1f;

    private bool timerActive = true;
    private bool gameWon = false;
    public bool gameReallyWon = false;
	void Start()
	{
		failed = false;
        teethTimer = this.GetComponent<TimerTeeth>();
	}
	
	// Update is called once per frame
	void Update () {
     
        //Debug.Log(overBrushTimer);
		//la.GetComponent<Renderer>().material.color = Color.white;
		if (cCount >= 0.05f) {
		//	la.SetActive (true);
			//laf.SetActive (false);
		//	ra.SetActive (true);
		//	raf.SetActive (false);
		}
		//ra.GetComponent<Renderer>().material.color = Color.white;
		if (what) {
			what = false;
			text.SetActive(false);
		}if (timerTeeth.GameWin == false )
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && prevKeyboardSetting != "Left")
            {
                //Move square down
                ty = new Vector3(0, -4, 0);
                tb.GetComponentInParent<Transform>().position += ty;
                cy = tb.GetComponentInParent<Transform>().position;
                cy += new Vector3(2.7f, 4.75f, 3.5f);
                Instantiate(ct, cy, Quaternion.identity);
                prevKeyboardSetting = "Left";
                //la.SetActive(false);
                laf.SetActive(false);
                raf.SetActive(true);
                cCount = 0.0f;
                vibrate.Play();
                //la.GetComponent<Renderer>().material.color = Color.red;
                count++;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && prevKeyboardSetting != "Right")
            {
                //Move square up and right 
                ty = new Vector3(0, 4, 0);
                tb.GetComponentInParent<Transform>().position += ty;
                tx = new Vector3(2, 0, 0);
                tb.GetComponentInParent<Transform>().position += tx;
                prevKeyboardSetting = "Right";
                //ra.SetActive(false);
                raf.SetActive(false);
                laf.SetActive(true);
                cCount = 0.0f;
                vibrate.Play();
                //ra.GetComponent<Renderer>().material.color = Color.red;
                count++;
            }
        }
		if (count == 17) {
			//text.SetActive (true);
			what = true;
            timerActive = false;
            overBrushTimer += Time.deltaTime;
            Debug.Log("called");
            //teethTimer.startTimer = false;
            gameWon = true;
           
        }
		if (count >= 18 && !gameWon /*&& tCount >= 3.9f*/) {
			failed = true;
            timerActive = false;
            
		}
		if (/*tCount >= 4.0f*/ overBrushTimer >= overBrushTimerThreshold && !failed && gameWon) {
            Debug.Log("Teeth clean");
            //play win sound
            winSound.SetActive (true);
           // gameWinSound.enabled = true;
			
			tCount = 0.0f;
			winPanel.SetActive(true);
			text.SetActive(false);
			timerTeeth.GameWin = true;
            gameWon = false;
            gameReallyWon = true;

			//Application.LoadLevel(3);
			//Go to next minigame and splash screen
		} else if (count >= 18) {
			//Debug.Log ("Your teeth are too clean. You got fired for being late.");
			tCount = 0.0f;
			fired.SetActive(true);
			text.SetActive(false);
		} else if(tCount >= 4.0f && failed){
			fail.SetActive(true);
			tCount = 0.0f;
			text.SetActive(false);
		}
		
        if (timerActive && !failed)
        {
            cCount += Time.deltaTime;
            tCount += Time.deltaTime;
        }
		//When it gets to int then end the scene

	}
}
