using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoffeeFilling : MonoBehaviour {


	public SpriteRenderer CoffeeEmpty;

	public Image CoffeeFull;

	public Image CoffeePour;

	float timeToFill = 4.0f;

	float t = 0.0f;
	
	float tElapsed;
	
	bool levelComplete = false;
	
	public GameObject winningText;
	
	public GameObject losingText;
	
	public GameObject Poop;

	public AudioSource winSound;

	public AudioSource fartSound;

	public bool terribleCoffee = false;

	public GameObject fartGameObject;

	public GameObject notEnoughCoffeePanel;

	public AudioSource coffeePourSound;

	public bool pourTheCoffee = true;

	public GameObject theCoffeePour;
	
	public TimerCoffee timercoffee;

    private bool notEnoughCoffee;



	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        
		t += Time.deltaTime / timeToFill;
		tElapsed += Time.deltaTime;
		CoffeeEmpty.color = new Color(1f,1f,1f,(Mathf.Lerp (1.0f, 0.5f, t)));
		CoffeeFull.fillMethod = Image.FillMethod.Vertical;

		if (pourTheCoffee) {
			CoffeeFull.fillAmount += Mathf.Lerp (0.0f, 1.0f, t * Time.deltaTime);
			CoffeePour.fillMethod = Image.FillMethod.Vertical;
		}
        if(tElapsed >= 1.25f && pourTheCoffee)
        {
		//	Debug.Log ("pourcoffe");
		CoffeePour.fillAmount -= Mathf.Lerp (0.0f, 0.54f, t * Time.deltaTime *1.4f );
			//pourTheCoffee = false;
        }

        if (Input.GetKey(KeyCode.Space) && tElapsed >= 2.5f && tElapsed <= 3.5f && !notEnoughCoffee)
        {
          
            // if(tElapsed >= 2.5f && tElapsed <= 3.5f )
            {
                if (levelComplete == false)
                {
                    // Debug.Log ("Right timing");
                    CoffeeFull.fillAmount = 1.0f;
                    winSound.Play();
                    winningText.SetActive(true);
                    levelComplete = true;

                    timercoffee.GameWin = true;

                }

            }
        }
          if(Input.GetKey(KeyCode.Space) && tElapsed < 2.5f) 
          {
            pourTheCoffee = false;
            notEnoughCoffee = true;
				notEnoughCoffeePanel.SetActive(true);
				coffeePourSound.Stop ();
				//CoffeeFull.fillAmount -= Mathf.Lerp (1.0f, 0.0f, t * Time.deltaTime );
				//CoffeeFull.fillAmount = 0.2f ;

				theCoffeePour.SetActive(false);
            //Application.LoadLevel("GameOverScene");
          }
        
        
        
        if(levelComplete == true)
        {
          if(tElapsed >= 4.5f)
          {
			Application.LoadLevel(4);
			}
		}
		

		if(tElapsed >= 4.0f)
		{
		   if(levelComplete == false)
		   {
			
			//Debug.Log ("Back to main menu");
			losingText.SetActive(true);
			Poop.SetActive(true);
				fartGameObject.SetActive(true);


//				if(tElapsed >= 5.0f)
//				{
//					Application.LoadLevel("GameOverScene");
			}
		 }

		if(tElapsed >= 5.0f)
		{
			Application.LoadLevel("GameOverScene");
		}
		
	}
}
