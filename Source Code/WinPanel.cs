using UnityEngine;
using System.Collections;

public class WinPanel : MonoBehaviour {

	public AudioSource winSound;
	//float tCount;
	void Start () {
		winSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {

			//Application.LoadLevel (4);

		//Needs to load next level which is random?
	}
}
