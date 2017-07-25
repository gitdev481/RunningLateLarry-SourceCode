using UnityEngine;
using System.Collections;

public class FinishGame : MonoBehaviour {

    public GameObject car;
    
    public AudioSource arrive;
    
    public TimerCoffee Timer;
    
    public CarMovement carMovement;
    
    float tTime;

	// Use this for initialization
	void Start () {
	
	//StartCoroutine(OnTriggerEnter2D(Collider2D car));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	 if(Timer.GameWin == true)
	 {
			car.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			carMovement.power = 0;
			tTime  += Time.deltaTime;
	 }
		if(tTime >= 1.0f){
			//yield return new  WaitForSeconds(1);
			
			Application.LoadLevel("EndScreen");
			
		}
	}
	
	void OnTriggerEnter2D(Collider2D car)
	
	{
		 Timer.GameWin = true;
	
	    arrive.Play();
	  
		
	
	}
}
