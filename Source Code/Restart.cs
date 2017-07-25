using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public void Clicked()
	{
       Destroy( GameObject.FindGameObjectWithTag("GLOBALTIMER"));
		Application.LoadLevel ("mainMenu");
        
	}
	
}
