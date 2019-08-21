using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameUIScript : MonoBehaviour {

    public bool isFullScreen;
    public GameObject instructions;
    public Animator instructAnimation;
  

    private void Start()
    {
        instructions.SetActive(false);
    }
    public void OnStartClick() {
        Debug.Log("startClicked");
        //SceneManager.LoadScene(0);
    }
    public void OnInstructClick() {
        instructions.SetActive(true);
        instructAnimation.SetBool("clicked",true);
        //play the animation

    }
    public void backBtn() {
        instructAnimation.SetBool("clicked",false);
       // instructions.SetActive(false);
        //playanimation to go back
    }
    public void OnQuitClick() {
        Debug.Log("quitClicked");
        Application.Quit();
    }

    public void onCheckFullscreen() {
        if (isFullScreen == true)
        {
            Debug.Log("fullscreenMode");
            isFullScreen = false;
            Screen.fullScreen = true;
            //fullscreen mode
        }
        else if (isFullScreen == false)
        {
            Debug.Log("windowedMode");
            isFullScreen = true;
            Screen.fullScreen = false;
        }

    }
}
