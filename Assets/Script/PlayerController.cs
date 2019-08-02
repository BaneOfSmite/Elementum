using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Transform CameraTransform;
	private bool isRotating, isGrounded;
	public float MovementSpeed, JumpSpeed;

	void Awake() {
		CameraTransform = Camera.main.transform;
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Q) && !isRotating) {
			StartCoroutine(Rotate(Vector3.up, 90, 1.0f));
		} else if (Input.GetKeyDown(KeyCode.E) && !isRotating) {
			StartCoroutine(Rotate(Vector3.up, -90, 1.0f));
		}

		transform.position += GetMoveVector().normalized * MovementSpeed * Time.deltaTime;
	}

	void FixedUpdate() {
		if (Input.GetAxisRaw("Jump") != 0 && isGrounded) {
			isGrounded = false;
			GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * JumpSpeed, ForceMode.Impulse);
		}
	}

	void OnCollisionStay() {
		isGrounded = true;
	}

	private Quaternion GetCameraTurn() {
		return Quaternion.AngleAxis(CameraTransform.rotation.eulerAngles.y, Vector3.up);
	}

	private Vector3 GetMoveVector() {
		Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		moveVector = GetCameraTurn() * moveVector;

		return moveVector;
	}

	IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
		isRotating = true;
		Quaternion from = transform.rotation;
		Quaternion to = transform.rotation;
		to *= Quaternion.Euler(axis * angle);

		float elapsed = 0.0f;
		while (elapsed < duration) {
			transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
			elapsed += Time.deltaTime;
			yield return null;
		}
		transform.rotation = to;
		isRotating = false;
	}
}