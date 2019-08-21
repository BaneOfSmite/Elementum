using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class gameUIScript : MonoBehaviour {

    public bool isFullScreen;
    public GameObject instructions;
    public GameObject settings;
    public Animator instructAnimation;
    public Animator settingsAnimation;
    public AudioMixer mixer;
  

    private void Start()
    {
        instructions.SetActive(false);
        settings.SetActive(false);
    }
    public void setVolume(float sliderValue) {
        mixer.SetFloat("BGM",Mathf.Log10(sliderValue)*20);
    }
    public void setQuality(string name) {
        switch (name)
        {
            case "low":
                QualitySettings.SetQualityLevel(0);
                break;
            case "medium":
                QualitySettings.SetQualityLevel(1);
                break;
            case "high":
                QualitySettings.SetQualityLevel(2);
                break;          
            default:
                name = "high";
                break;
        }

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
    public void OnSettingsClick() {
        settings.SetActive(true);
        settingsAnimation.SetBool("settingsPress", true);
    }
    public void backBtn() {
        instructAnimation.SetBool("clicked",false);
        settingsAnimation.SetBool("settingsPress",false);
        
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
