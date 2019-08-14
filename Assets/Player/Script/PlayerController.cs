using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance;
    private Transform CameraTransform;
    private bool isRotating, isGrounded;
    public float MovementSpeed, JumpSpeed;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        CameraTransform = Camera.main.transform;
    }

    void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            transform.Rotate(new Vector3(0, 0.5f, 0));
        } else if (Input.GetKey(KeyCode.E)) {
            transform.Rotate(new Vector3(0, -0.5f, 0));
        }
        transform.position += GetMoveVector().normalized * MovementSpeed * Time.deltaTime;
    }

    void FixedUpdate() {
        if (Input.GetAxisRaw("Jump") != 0 && isGrounded) {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * JumpSpeed, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == ("Floor") && isGrounded == false) {
            isGrounded = true;
        }
    }

    private Quaternion GetCameraTurn() {
        return Quaternion.AngleAxis(CameraTransform.rotation.eulerAngles.y, Vector3.up);
    }

    private Vector3 GetMoveVector() {
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVector = GetCameraTurn() * moveVector;
        return moveVector;
    }
}