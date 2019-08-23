using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	public bool isEnding;
	private void OnTriggerEnter(Collider hit) {
		if (hit.CompareTag("Player")) {
			if (isEnding) {
				GameManager.Instance.GameOver();
			}
			hit.transform.position = transform.GetChild(0).transform.position;

			if (gameObject.name.Contains("FireRealm"))
			{
				GameManager.Instance.source.clip = GameManager.Instance.music[0];
				GameManager.Instance.source.Play();
			}
		}
	}
}