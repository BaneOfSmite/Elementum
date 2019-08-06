using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {
    public enum PlayerFilter { Air, Water, Earth, Fire, Lightning }
    public List<PlayerFilter> Have;
    public Color[] FilterColor;
    public PlayerFilter CurrentType;
    private GameManager Manager;
    private int cycle = 0;

    void Start() {
        Manager = GameManager.Instance;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (cycle != (Have.ToArray().Length - 1)) {
                cycle++;
            } else {
                cycle = 0;
            }
            CurrentType = Have[cycle];
            GameManager.Instance.Filter.color = FilterColor[(byte)CurrentType];
        }
    }

    private void OnCollisionEnter(Collision hit) {
        if (hit.gameObject.CompareTag("EnemyBattle")) {
            Vector3 dir = hit.contacts[0].point - transform.position;
            dir = (-dir + new Vector3(0, 1f, 0)).normalized;
            GetComponent<Rigidbody>().AddForce(dir * 250);
            Manager.PlayerHealth -= hit.gameObject.GetComponent<EnemyBattle>().Damage;
        }
        if (hit.gameObject.name.Equals("Fire")) {
            Destroy(hit.gameObject);
            Have.Add(PlayerFilter.Fire);
        } else if (hit.gameObject.name.Equals("Air")) {
            Destroy(hit.gameObject);
            Have.Add(PlayerFilter.Air);
        } else if (hit.gameObject.name.Equals("Earth")) {
            Destroy(hit.gameObject);
            Have.Add(PlayerFilter.Earth);
        } else if (hit.gameObject.name.Equals("Lightning")) {
            Destroy(hit.gameObject);
            Have.Add(PlayerFilter.Lightning);
        }
    }
}