using UnityEngine;
using System.Collections;

public class GameOverSound : MonoBehaviour {
	public AudioSource gameOver;

	// Use this for initialization
	void Start () {
		gameOver.Play ();
	}
	

}
