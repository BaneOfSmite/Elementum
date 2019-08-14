using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveRenderer : MonoBehaviour {
	public PlayerBattle _PlayerBattle;
	void Start() {
		_PlayerBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBattle>();
	}
	void Update() {
		if (gameObject.name == "Fire" && _PlayerBattle.CurrentType == PlayerBattle.PlayerFilter.Water) {
			SetInvis();
		} else if (gameObject.name == "Water" && _PlayerBattle.CurrentType == PlayerBattle.PlayerFilter.Earth) {
			SetInvis();
		} else if (gameObject.name == "Earth" && _PlayerBattle.CurrentType == PlayerBattle.PlayerFilter.Air) {
			SetInvis();
		} else if (gameObject.name == "Air" && _PlayerBattle.CurrentType == PlayerBattle.PlayerFilter.Fire) {
			SetInvis();
		} else {
			transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
			transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
		}
	}
	private void SetInvis() {
		transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
		transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
	}
}