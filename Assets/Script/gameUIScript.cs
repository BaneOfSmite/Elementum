using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class gameUIScript : MonoBehaviour
{

    public bool isFullScreen;
    public GameObject instructions;
    public GameObject settings;
    public Animator instructAnimation;
    public Animator settingsAnimation;
    public AudioMixer mixer;
    public GameObject LoadingBar;
    public GameObject[] disabledd;
    private AsyncOperation ao;
    private bool isloading;


    private void Start()
    {
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    void Update()
    {
        if (isloading)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            LoadingBar.transform.GetChild(0).GetComponent<Slider>().value = progress;
        }
    }
    public void setVolume(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }
    public void setQuality(string name)
    {
        switch (name)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0);
                break;
            case "Medium":
                QualitySettings.SetQualityLevel(1);
                break;
            case "High":
                QualitySettings.SetQualityLevel(2);
                break;
            default:
                name = "High";
                break;
        }

    }
    public void OnStartClick()
    {
        Debug.Log("startClicked");
        foreach (GameObject i in disabledd)
        {
            i.SetActive(false);
        }
        ao = SceneManager.LoadSceneAsync(1);
        LoadingBar.SetActive(true);
        isloading = true;
    }
    public void OnInstructClick()
    {
        instructions.SetActive(true);
        instructAnimation.SetBool("clicked", true);
        //play the animation
    }
    public void OnSettingsClick()
    {
        settings.SetActive(true);
        settingsAnimation.SetBool("settingsPress", true);
    }
    public void backBtn()
    {
        instructAnimation.SetBool("clicked", false);
        settingsAnimation.SetBool("settingsPress", false);
        // instructions.SetActive(false);
        //playanimation to go back
    }
    public void OnQuitClick()
    {
        Debug.Log("quitClicked");
        Application.Quit();
    }

    public void onCheckFullscreen()
    {
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
            Screen.fullScreen = false;//windowedMode
        }

    }
}
