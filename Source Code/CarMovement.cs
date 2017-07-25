using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	float xspeep = 0f;
	public float power = 0.005f;
	float friction = 0.95f;
	float steering = 2.0f;
	bool forward = false;
	bool backward = false;
	
	
	public float fuel = 2;
	
	
	// Use this for initialization
	void FixedUpdate () {
		
		
		if(forward){
			xspeep += power;
			fuel -= power;
		}
		if(backward){
			xspeep -= power;
			fuel -= power;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	    
	    if (GetComponent<TimerCoffee>().GameWin == false)
	    {
	
	    float turn = Input.GetAxis("Horizontal")*steering;
		
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			forward = true;
		}
		if(Input.GetKeyUp(KeyCode.UpArrow)){
			forward = false;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			backward = true;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)){
			backward = false;
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
		
			transform.Rotate(0,0,turn);
		}
		
		}
		
		if(fuel < 0){
			
			xspeep = 0;
			
		}
		
		xspeep *= friction;
		transform.Translate(Vector2.right * -xspeep);
		
	}
}

