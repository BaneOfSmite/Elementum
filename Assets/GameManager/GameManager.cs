using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public float PlayerHealth, PlayerMana;
	public static GameManager Instance;
	public Image Filter;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}
	void Update() {
		if (PlayerHealth <= 0) {
			PlayerHealth = 0;
			//Death
		}
		//UpdateUI
	}
}