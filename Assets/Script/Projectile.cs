using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public enum AttackTypes { Air, Water, Earth, Fire, Lightning }
	public AttackTypes AttackType;
	// Use this for initialization
	void Start() {
		Destroy(gameObject, 1);
	}

	// Update is called once per frame
	void Update() {
		if (AttackType == AttackTypes.Water) {

		}
	}
	private void OnTriggerEnter2D(Collider2D hit) {
		if (hit.CompareTag("Enemy")) {
			if (AttackType == AttackTypes.Fire) {
				if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Air) {
					hit.GetComponent<EnemyBattle>().Health -= 20;
				} else if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Water) {
					hit.GetComponent<EnemyBattle>().Health -= 5;
				} else {
					hit.GetComponent<EnemyBattle>().Health -= 10;
				}
			}
		}
	}
}