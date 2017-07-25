using UnityEngine;
using System.Collections;

public class RememberMe : MonoBehaviour
{
    public string savedUsername;
    public string savedPassword;
    public GLOBALTIMER globalTimer;
    public bool shouldUserBeRemembered = false;
    // Use this for initialization
    void Start()
    {
       
      
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Application.loadedLevel == 6 && GameObject.FindWithTag("GLOBALTIMER") != null)
        {
            globalTimer = GameObject.FindWithTag("GLOBALTIMER").GetComponent<GLOBALTIMER>();

        }
        if (Application.loadedLevel == 1)
        {
            shouldUserBeRemembered = false;
        }
        if (globalTimer != null && shouldUserBeRemembered)
        {
            savedUsername = globalTimer.nameInputField.text;
            savedPassword = globalTimer.passwordInputField.text;
        }
        if(!shouldUserBeRemembered && globalTimer != null && globalTimer.actualNameInputField != null && globalTimer.actualPasswordInputfield != null)
        {
            globalTimer.actualNameInputField.text = savedUsername;
            globalTimer.actualPasswordInputfield.text = savedPassword;
           // globalTimer.nameInputField.text = "AAA";
            //globalTimer.passwordInputField.text = "AAA";
            //Debug.Log("AAA");
            shouldUserBeRemembered = true;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
