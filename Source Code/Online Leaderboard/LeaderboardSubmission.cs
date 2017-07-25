using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardSubmission : MonoBehaviour {

   
    public string localName;
    public float localScore;
    public string localPassword;
    public GLOBALTIMER globalTimer;
    public HSController HSController;

    private bool startLoading;
    private float loadThreshold = 0.5f;
    private float loadTimer = 0f;
    public bool setTheScore = false;
    private bool startScoring = false;
    public Text notificationBox;
    private float roundedScore;
    private bool scoresPosted = false;
    private bool successStarted = false;
    private string successText;
    private bool setSuccessText = false;
    private string[] badWords;
   // private bool shouldNotificationBeCleared = false;
    private float notificationThreshold = 2f;
    private float notificationTimer;
    private bool startNotificationTimer = false;
    public RememberMe rememberMe;

    void Start()
    {
        badWords = new string[] { "shit", "cunt", "cock", "fuck", "asshole", "bitch", "hell", "nigger", "bastard", "faggot", "suck", "dick", "crap", "rape", "rapist", "pussy"};
        startLoading = true;
        loadTimer = 0f;
        if (Application.loadedLevel == 6 && GameObject.FindWithTag("GLOBALTIMER") != null && GameObject.FindWithTag("REMEMBERME") != null)
        {
            globalTimer = GameObject.FindWithTag("GLOBALTIMER").GetComponent<GLOBALTIMER>();
            rememberMe = GameObject.FindWithTag("REMEMBERME").GetComponent<RememberMe>();
            roundedScore = Mathf.Round(globalTimer.globalTimer * 1000f) / 1000f;
        }
     
    }
    void Update()
    {
        if(startNotificationTimer)
        {
            notificationTimer += Time.deltaTime;
        }
        if(notificationTimer >= notificationThreshold)
        {
            notificationBox.text = "";
            startNotificationTimer = false;
            notificationTimer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SubmitLeaderboardScore();
        }
        if (startLoading)
        {
            loadTimer += Time.deltaTime;
          
        }
        if(loadTimer >= loadThreshold)
        {
           
            HSController.startGetScores();
            startLoading = false;
            loadTimer = 0f;
        }


        if (setTheScore)
        {
            if (!startLoading)
            {
                if (localPassword == HSController.retrievedID || HSController.retrievedID == "" || HSController.retrievedID == null)
                {
                    HSController.startGetCurrentScore();
                    startLoading = true;
                    startScoring = true;
                }
                else
                {
                    notificationBox.text = "WRONG PASSWORD!";
                    ClearNotificationBox();
                  //  Debug.Log("WRONG PASSWORD!");
                }
                setTheScore = false;
            }
        }

        if (startScoring && !startLoading)
        {
           // Debug.Log("LOCAL SCORE: " + localScore);
            //Debug.Log("RETRIEVED SCORE: " + HSController.retrievedScore);

            if (localScore < HSController.retrievedScore)
            {
                
                HSController.startPostScores();
                scoresPosted = true;
                startLoading = true;
                
            }
            else
            {
                notificationBox.text = "your best is higher or equal!";
                ClearNotificationBox();
              //  Debug.Log("SCORE IS LOWER");
            }
            startScoring = false;
        }
        if(scoresPosted && !startLoading && successStarted)
        {
            HSController.startGetScores();
         
            successText = "SUCCESS!";
           
            scoresPosted = false;
            successStarted = false;
            setSuccessText = true;
            rememberMe.shouldUserBeRemembered = true;
            

        }
        if(setSuccessText)
        {
            //setting the text like this because setting the text after HSController.startGetScores() caused a null reference exception.
            notificationBox.text = successText;
            ClearNotificationBox();
        }
    }
   
    public void ClearNotificationBox()
    {
       // Debug.Log("CLEARBOX");
        startNotificationTimer = true;
        setSuccessText = false;
    }   
    
    public void GetScoresButton()
    {
        HSController.startGetScores();
    }
    public void UpdateFields() {
        localName = globalTimer.nameInputField.text;
        localScore =  roundedScore;
        localPassword = globalTimer.passwordInputField.text;
    }

    public void SubmitLeaderboardScore()
    {
        successStarted = true;
        UpdateFields();
        if (localName == "")
        {
            notificationBox.text = "ENTER A NAME!";
            ClearNotificationBox();
           // Debug.Log("ENTER A NAME!");
            return;
        }
        if (localPassword == "")
        {
            notificationBox.text = "ENTER A PASSWORD!";
            ClearNotificationBox();
            return;
        }

        for (int i = 0; i < badWords.Length; i++)
        {
            if (localName.Contains(badWords[i]))
            {
                Debug.Log("inapproriate username");

                notificationBox.text = "REJECTED:\nTASTELESS\nUSERNAME!";
                ClearNotificationBox();
                return;
            }
        }

        HSController.updateOnlineHighscoreData(localName, localScore, localPassword);
        HSController.startGetCurrentID();
       
       // Debug.Log("LOCALUNIQUEID" + localPassword);
       // Debug.Log("RETRIEVED ID: " + HSController.retrievedID);

        setTheScore = true;
        startLoading = true;
    }
    public void GetCurrentScore()
    {
        UpdateFields();
        HSController.updateOnlineHighscoreData(localName, localScore, localPassword);
        HSController.startGetCurrentScore();
    }


}
