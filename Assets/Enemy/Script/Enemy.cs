using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum EnemyType {
        Air, Water, Earth, Fire, Lightning
    };
    public EnemyType Type;
    public float MovementSpeed, Health, ActivationDistance;
    public GameObject Minions;
    private GameObject Player;
    // Use this for initialization
    void Start() {
        Player = PlayerController.Instance.gameObject;
    }

    // Update is called once per frame
    void Update() {
		if (Vector3.Distance(Player.transform.position, transform.position) <= ActivationDistance) {
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        transform.position += dir * MovementSpeed * Time.deltaTime;
        transform.forward = dir;
    }
}
}
