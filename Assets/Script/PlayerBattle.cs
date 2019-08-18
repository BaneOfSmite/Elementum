using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {
    private GameManager Manager;
    public GameObject[] Attacks;
    void Start() {
        Manager = GameManager.Instance;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (Manager.CurrentType == GameManager.PlayerFilter.Fire) {
                Instantiate(Attacks[0], new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
            } else if (Manager.CurrentType == GameManager.PlayerFilter.Water) {
                Instantiate(Attacks[1], transform.position, Quaternion.identity);
            }
        }
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized * 1f * Time.deltaTime;
    }
}