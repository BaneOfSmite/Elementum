using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUIScript : MonoBehaviour {

	
	void Start () {

		
	}	
	void Update () {
		
	}


    public void OnStartClick() {
        Debug.Log("startClicked");

    }

    public void OnQuitClick() {
        Debug.Log("quitClicked");
        Application.Quit();
    }
}
