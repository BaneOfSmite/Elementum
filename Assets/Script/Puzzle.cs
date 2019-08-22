using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

	public bool isFilled;
	void Start () {
		
	}
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.name == "PuzzleBlock") {
			if (!isFilled) {
				isFilled = true;
				if (gameObject.name == "Fail") {
					GameManager.Instance.PuzzleFail = true;
				}
				else {
					GameManager.Instance.PuzzleBlockLeft--;
				}
			}
		}
	}
	
}
