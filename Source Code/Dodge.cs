using UnityEngine;
using System.Collections;

public class Dodge : MonoBehaviour {
	/*
	float speed = 0f;
	Vector3 move;
	// Use this for initialization
	void Start () {
		speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			move = new Vector3(-1, 0, 0);
			GetComponentInParent<Transform>().position += move;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			move = new Vector3(1, 0, 0);
			GetComponentInParent<Transform>().position += move;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			move = new Vector3(0, -1, 0);
			GetComponentInParent<Transform>().position += move;
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			move = new Vector3(0, 1, 0);
			GetComponentInParent<Transform>().position += move;
		}
	}*/


	public Rigidbody2D rb;
	float lockTime;
	float tCount;

	void Update()
	{
		if (lockTime >= 100) {
			Application.LoadLevel(3);
		}
		if (tCount >= 4.0f) {
			//Application.LoadLevel(2);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce (new Vector2(-10, 0));
			rb.AddForce (new Vector2(0, 5));
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce (new Vector2(10, 0));
			rb.AddForce (new Vector2(0, -5));
		}
		else if (Input.GetKey (KeyCode.UpArrow)) {
			rb.AddForce (new Vector2(0, 10));
			rb.AddForce (new Vector2(-5, 0));
		}
		else if (Input.GetKey (KeyCode.DownArrow)) {
			rb.AddForce (new Vector2(0, -10));
			rb.AddForce (new Vector2(5, 0));
		}
		tCount += Time.deltaTime;
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		lockTime += 1;
	}

}
