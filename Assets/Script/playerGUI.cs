using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerGUI : MonoBehaviour {

    public GameObject healthBar;
    public GameObject manaBar;

	void Update () {
        //link with game manager screen
        healthBar.GetComponent<Image>().fillAmount = GameManager.Instance.PlayerHealth / 100;
        manaBar.GetComponent<Image>().fillAmount = GameManager.Instance.CurrentMana / 100;





    }
}
