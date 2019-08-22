using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerGUI : MonoBehaviour {

    public GameObject healthBar;
    public GameObject manaBar;
    public TextMeshProUGUI points;
    void Update() {
        healthBar.GetComponent<Image>().fillAmount = GameManager.Instance.PlayerHealth / 100;
        manaBar.GetComponent<Image>().fillAmount = GameManager.Instance.CurrentMana / 100;
        points.text = GameManager.Instance.ObjectiveCollected.ToString();
    }
}