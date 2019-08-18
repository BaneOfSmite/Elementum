using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {
    private GameManager Manager;
    public GameObject[] Attacks;
    public float speed;
    public bool isGrounded;
    void Start() {
        Manager = GameManager.Instance;
    }

    void Update() {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R)) {
            if (Manager.CurrentType == GameManager.PlayerFilter.Fire) {
                Instantiate(Attacks[0], new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
            } else if (Manager.CurrentType == GameManager.PlayerFilter.Water) {
                Instantiate(Attacks[1], transform.position, Quaternion.identity);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.CompareTag("Floor")) {
            isGrounded = true;
        }
    }
    void FixedUpdate() {
        if (Input.GetAxis("Jump") > 0 && isGrounded) {
            isGrounded = false;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 150);
        }
    }
}