using UnityEngine;

public class GameManager : MonoBehaviour {
	public float PlayerHealth, PlayerMana;
	public static GameManager Instance;

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