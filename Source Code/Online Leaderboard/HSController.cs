using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//Code influenced from Appymole's implementation http://mole.bplaced.net/?page_id=24
public class HSController : MonoBehaviour
{

    private const string FACEBOOK_APP_ID = "996067953792335";
    private const string FACEBOOK_URL = "http://www.facebook.com/dialog/feed";

    


    private static HSController instance6;
    public GLOBALTIMER globalTimer;
    public static HSController Instance
    {
        get { return instance6; }
    }

    private string secretKey = "123456789"; // Edit this value and make sure it's the same as the one stored on the server
    string addScoreURL = "julianstopher.bplaced.net/addscore.php?"; //be sure to add a ? to your url
    private string highscoreURL = "julianstopher.bplaced.net/display.php?";
    string getCurrentScoreURL = "julianstopher.bplaced.net/getCurrentScore.php?";
    string getCurrentPasswordURL = "julianstopher.bplaced.net/getCurrentPassword.php?";

    //for testing
    public string   password;
    public string   name3;
    float           score;
    public float retrievedScore = 100f;
    public string retrievedID;

    public string[] onlineHighscore;

    void Awake()
    {
       

        DontDestroyOnLoad(gameObject);
        // If no player ever existed, we are it.
        if (instance6 == null)
            instance6 = this;
        // If one already exists, its because it came from another level.
        else if (instance6 != this)
        {
            Destroy(gameObject);
            return;
        }
        if (Application.loadedLevel == 6 && GameObject.FindWithTag("GLOBALTIMER") != null)
        {
            globalTimer = GameObject.FindWithTag("GLOBALTIMER").GetComponent<GLOBALTIMER>();
        }

    }
  
   

    public void startGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void startPostScores()
    {
        StartCoroutine(PostScores());
    }
    public void startGetCurrentScore()
    {
        StartCoroutine(GetCurrentScore());
    }
    public void startGetCurrentID()
    {
        StartCoroutine(GetCurrentID());
    }

    //set actual values before posting score
    public void updateOnlineHighscoreData(string retrievedName, float retrievedScore, string retrievedPassword)
    {
        // uniqueID,name3 and score will get the actual value before posting score
        password = retrievedPassword; 
        name3 = retrievedName;
        score = retrievedScore;
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    
    IEnumerator PostScores()
    {
      
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        string hash = Md5Sum(name3 + score + secretKey);
        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name3) + "&score=" + score + "&uniqueID=" + password + "&hash=" + hash;
    
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW("http://" + post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }
    
    IEnumerator GetScores()
    {
        Scrolllist.Instance.loading = true;

        WWW hs_get = new WWW("http://" + highscoreURL);
       
        yield return hs_get;

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting the high score: " + hs_get.error);
            Scrolllist.Instance.loading = false;
            Scrolllist.Instance.getScrollEntrys();
        }
        else
        {

            //Change .text into string to use Substring and Split
            string help = hs_get.text;
            //200 is maximum length of highscore - 100 Positions (name+score)

            onlineHighscore = help.Split(";"[0]);

        }
        Scrolllist.Instance.loading = false;
        Scrolllist.Instance.getScrollEntrys();
    }

    IEnumerator GetCurrentScore()
    {
        Scrolllist.Instance.loading = true;
        string name_url = getCurrentScoreURL + "name=" + WWW.EscapeURL(name3);
       WWW hs_get = new WWW("http://" + name_url);

        yield return hs_get;

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting your current high score: " + hs_get.error);

        }
        else
        {

            string help = hs_get.text;
            float result;
          
            if(float.TryParse(help,out result)){
                retrievedScore = result;
            }
                

        }
        Scrolllist.Instance.loading = false;
        Scrolllist.Instance.getScrollEntrys();
    }

    IEnumerator GetCurrentID()
    {
        Scrolllist.Instance.loading = true;
        string ID_url = getCurrentPasswordURL + "name=" + WWW.EscapeURL(name3);
        WWW hs_ID = new WWW("http://" + ID_url);

        yield return hs_ID;

        if (hs_ID.error != null)
        {
            Debug.Log("There was an error getting your current password: " + hs_ID.error);

        }
        else
        {
          retrievedID = hs_ID.text;
          //  Debug.Log("the retriever = " + retrievedID);

           
        }
        Scrolllist.Instance.loading = false;
        Scrolllist.Instance.getScrollEntrys();


    }


    public void ShareToFacebook(string linkParameter, string nameParameter, string captionParameter, string descriptionParameter, string pictureParameter, string redirectParameter)
    {
        Application.OpenURL(FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID +
        "&link=" + WWW.EscapeURL(linkParameter) +
        "&name=" + WWW.EscapeURL(nameParameter) +
        "&caption=" + WWW.EscapeURL(captionParameter) +
        "&description=" + WWW.EscapeURL(descriptionParameter) +
        "&picture=" + WWW.EscapeURL(pictureParameter) +
        "&redirect_uri=" + WWW.EscapeURL(redirectParameter));

      

    }
    public void FacebookButtonClicked()
    {
        if (globalTimer != null)
        {
            ShareToFacebook("http://globalgamejam.org/2016/games/running-late-larry", "I'm playing Running Late Larry!", "New high score!", "I just a achieved a new score of " + globalTimer.globalTimer.ToString() + " in Running Late Larry!", "http://imgur.com/1axwzb6", "http://www.facebook.com/");
        }
    }
}
