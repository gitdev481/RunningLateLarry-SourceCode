using UnityEngine;
using System.Collections;

public class PlayerInputs : MonoBehaviour {

	// set player number from 1-4
	public int playerNumber;

	// get X axis from -1 to +1
	public float x {
		get {
			return Input.GetAxis(string.Format("Player{0}X", playerNumber));
		}
	}

	// get raw X axis from -1 to +1
	public float rawX {
		get {
			return Input.GetAxisRaw(string.Format("Player{0}X", playerNumber));
		}
	}

	// get Y axis from -1 to +1
	public float y {
		get {
			return Input.GetAxis(string.Format("Player{0}Y", playerNumber));
		}
	}

	// get raw Y axis from -1 to +1
	public float rawY {
		get {
			return Input.GetAxisRaw(string.Format("Player{0}Y", playerNumber));
		}
	}
}
