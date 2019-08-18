using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum EnemyType { Air, Water, Earth, Fire, Lightning }
    public EnemyType Type;
    public float ActivationDistance;
    private GameObject Player;

    void Start() {
        Player = PlayerController.Instance.gameObject;
    }

    void Update() {
        if (Vector3.Distance(Player.transform.position, transform.position) <= ActivationDistance) {
            GameManager.Instance.TriggerBattle(Type, gameObject);
        }
    }
}