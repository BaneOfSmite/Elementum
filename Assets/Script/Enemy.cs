using UnityEngine;
/*
To Do For This Script:
- Bug Test
 */
public class Enemy : MonoBehaviour {
    public EnemyBattle.EnemyType Type;
    public float ActivationDistance;
    private GameObject Player;

    void Start() {
        Player = PlayerController.Instance.gameObject;
    }

    void Update() {
        if (Vector3.Distance(Player.transform.position, transform.position) <= ActivationDistance) {
            GameManager.Instance.TriggerBattle(Type, gameObject, transform.childCount);
        }
    }
}