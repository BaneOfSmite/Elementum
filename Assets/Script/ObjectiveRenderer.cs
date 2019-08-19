using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveRenderer : MonoBehaviour {
	public GameManager Manager;

	void Start() {
		Manager = GameManager.Instance;
	}
	void Update() {
		transform.GetChild(0).Rotate(new Vector3(1, 1, 1));
		transform.GetChild(1).Rotate(new Vector3(1, 1, 1));
		transform.GetChild(2).Rotate(new Vector3(-1, -1, -1));

		if (gameObject.name == "Fire" && Manager.CurrentType == GameManager.PlayerFilter.Water) {
			SetInvis();
		} else if (gameObject.name == "Water" && Manager.CurrentType == GameManager.PlayerFilter.Earth) {
			SetInvis();
		} else if (gameObject.name == "Earth" && Manager.CurrentType == GameManager.PlayerFilter.Air) {
			SetInvis();
		} else if (gameObject.name == "Air" && Manager.CurrentType == GameManager.PlayerFilter.Fire) {
			SetInvis();
		} else {
			//transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
			//transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
			//transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
		}

	}
	private void SetInvis() {
		transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
		transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
		transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
	}
}