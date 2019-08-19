using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script:
- Bug Test
 */

public class Projectile : MonoBehaviour {
	public enum AttackTypes { Air, Water, Earth, Fire, Lightning, EarthDummy }
	public AttackTypes AttackType;
	private Vector3 _dir;
	public GameObject spike;
	private Vector3 SpawnLocation;
	void Awake() {
		SpawnLocation = transform.position;
		SpawnLocation.x += _dir.x;
		SpawnLocation.y -= 0.5f;
	}
	void Start() {
		if (AttackType == AttackTypes.Earth || AttackType == AttackTypes.Air) {
			Destroy(gameObject, 10);
		} else {
			Destroy(gameObject, 1);
		}
		if (AttackType == AttackTypes.Earth) {
			StartCoroutine(SummonSpikes());
		}
	}

	void Update() {
		if (AttackType == AttackTypes.Water || AttackType == AttackTypes.Air) {
			transform.position += _dir * 5 * Time.deltaTime;
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
			} else if (AttackType == AttackTypes.Water) {
				if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Fire) {
					hit.GetComponent<EnemyBattle>().Health -= 20;
				} else if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Earth) {
					hit.GetComponent<EnemyBattle>().Health -= 5;
				} else {
					hit.GetComponent<EnemyBattle>().Health -= 10;
				}
				Destroy(gameObject);
			} else if (AttackType == AttackTypes.Earth || AttackType == AttackTypes.EarthDummy) {
				if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Water) {
					hit.GetComponent<EnemyBattle>().Health -= 20;
				} else if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Air) {
					hit.GetComponent<EnemyBattle>().Health -= 5;
				} else {
					hit.GetComponent<EnemyBattle>().Health -= 10;
				}
			} else if (AttackType == AttackTypes.Air) {
				if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Earth) {
					hit.GetComponent<EnemyBattle>().Health -= 20;
				} else if (hit.GetComponent<EnemyBattle>().Type == EnemyBattle.EnemyType.Fire) {
					hit.GetComponent<EnemyBattle>().Health -= 5;
				} else {
					hit.GetComponent<EnemyBattle>().Health -= 10;
				}
			}
		}
	}
	public void setDir(Vector3 dir) {
		_dir = dir.normalized;
	}
	private IEnumerator SummonSpikes() {
		for (int i = 0; i < 10; i++) {
			Instantiate(spike, SpawnLocation, Quaternion.identity);
			SpawnLocation.x += _dir.x;
			yield return new WaitForSeconds(0.25f);
		}
	}
}