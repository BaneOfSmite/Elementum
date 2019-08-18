using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public float PlayerHealth, PlayerMaxMana = 100, CurrentMana = 100;
	public static GameManager Instance;
	public bool isBattle;
	public Image Filter;
	public enum PlayerFilter { Air, Water, Earth, Fire, Lightning, None }
	public List<PlayerFilter> Have;
	public Color[] FilterColor;
	public PlayerFilter CurrentType;
	private int cycle = 0;
	public GameObject[] Scenes;
	private GameObject EnemyTrigger;
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
		if (CurrentType != PlayerFilter.None) {
			CurrentMana -= Time.deltaTime * 2.5f;
			if (CurrentMana <= 0) {
				CurrentMana = 0f;
				cycle = 0;
				CurrentType = Have[cycle];
				Filter.color = FilterColor[(byte) CurrentType];
			}
		} else {
			if (CurrentMana <= PlayerMaxMana) {
				CurrentMana += Time.deltaTime;
				if (CurrentMana >= PlayerMaxMana) {
					CurrentMana = 100f;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			if (cycle != (Have.ToArray().Length - 1)) {
				cycle++;
			} else {
				cycle = 0;
			}
			CurrentType = Have[cycle];
			Filter.color = FilterColor[(byte) CurrentType];
		}
		//UpdateUI
	}
	public void TriggerBattle(Enemy.EnemyType Type, GameObject Triggerer) {
		Scenes[0].SetActive(false);
		Scenes[1].SetActive(true);
		isBattle = true;
		EnemyTrigger = Triggerer;
		//Spawn Enemy In Battle Scene
	}
	public void BattleEnd(GameObject _Enemy) {
		Destroy(EnemyTrigger);
		Destroy(_Enemy);
		Scenes[1].SetActive(false);
		Scenes[0].SetActive(true);
	}
}