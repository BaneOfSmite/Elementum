using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
To Do For This Script:
- Limit Firerate
- Bug Test
- Animation
 */
public class PlayerBattle : MonoBehaviour {
    private GameManager Manager;
    public GameObject[] Attacks;
    public float speed;
    public bool isGrounded;
    private bool isFlipped;
    void Start() {
        Manager = GameManager.Instance;
    }

    void Update() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = false;
        } else if (Input.GetAxisRaw("Horizontal") < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = true;
        }
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R)) {
            if (Manager.CurrentType == GameManager.PlayerFilter.Fire) {
                Instantiate(Attacks[0], new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
            } else if (Manager.CurrentType == GameManager.PlayerFilter.Water) {
                Instantiate(Attacks[1], transform.position, Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
            } else if (Manager.CurrentType == GameManager.PlayerFilter.Earth) {
                Instantiate(Attacks[2], transform.position, Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
            } else if (Manager.CurrentType == GameManager.PlayerFilter.Air) {
                Instantiate(Attacks[3], transform.position, Quaternion.identity).GetComponent<Projectile>().setDir((isFlipped ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)));
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