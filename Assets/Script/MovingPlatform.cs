using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script:
- Bug Test
 */
public class MovingPlatform : MonoBehaviour {

	public List<Vector3> Position;
	private bool Direction;
	public float speed;
	private Vector3 _dir;

	// Use this for initialization
	void Start() {
		Position.Add(transform.GetChild(0).position);
		Position.Add(transform.GetChild(1).position);
		_dir = Position[1] - transform.position;
	}

	// Update is called once per frame
	void Update() {
		transform.position += _dir.normalized * speed * Time.deltaTime;
		if (Direction) {
			if (Vector3Int.RoundToInt(transform.position) == Vector3Int.RoundToInt(Position[0])) {
				_dir = Position[1] - transform.position;
				Direction = false;
			}
		}

		if (!Direction) {
			if (Vector3Int.RoundToInt(transform.position) == Vector3Int.RoundToInt(Position[1])) {
				_dir = Position[0] - transform.position;
				Direction = true;
			}
		}
	}
}