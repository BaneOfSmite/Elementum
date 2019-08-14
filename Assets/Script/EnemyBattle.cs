using UnityEngine;

public class EnemyBattle : MonoBehaviour {
	public float Damage, Health;
	public enum EnemyType { Air, Water, Earth, Fire, Lightning }
	public EnemyType Type;
	void Start() {
		switch (Type) {
			case EnemyType.Air:
				Damage = 0;
				break;
			case EnemyType.Water:
				Damage = 0;
				break;
			case EnemyType.Earth:
				Damage = 0;
				break;
			case EnemyType.Fire:
				Damage = 0;
				break;
			case EnemyType.Lightning:
				Damage = 0;
				break;
		}
	}
	void Update() {
		if (Health <= 0) {
			Health = 0;

		}
		switch (Type) {
			case EnemyType.Air:

				break;
			case EnemyType.Water:

				break;
			case EnemyType.Earth:

				break;
			case EnemyType.Fire:

				break;
			case EnemyType.Lightning:

				break;
		}
	}
}