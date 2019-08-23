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
    public GameObject ExploreScene;

    void Start() {
        Position.Add(transform.GetChild(0).position);
        Position.Add(transform.GetChild(1).position);
        _dir = Position[1] - transform.position;
    }

    void Update() {
        transform.Rotate(0, 0, 1);

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
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.transform.position += _dir.normalized * speed * Time.deltaTime;
        } else if (other.gameObject.name.Contains("Air")) {
            other.gameObject.transform.position += _dir.normalized * speed * Time.deltaTime;
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = ExploreScene.transform;
            other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
	}*/
}