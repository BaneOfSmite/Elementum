using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	private void OnTriggerEnter(Collider hit) {
		if (hit.CompareTag("Player")) {
			hit.transform.position = transform.GetChild(0).transform.position;
		}
	}
}
