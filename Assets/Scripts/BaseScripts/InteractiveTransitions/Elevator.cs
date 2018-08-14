using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	Rigidbody2D rb;
	float direction;
	public float movespeed = 2f;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		movespeed = -movespeed;
	}

	void Update () {
		rb.velocity = new Vector2 (rb.velocity.x, direction * movespeed);
	}

	public void StartMove (float input_direction) {
		direction = input_direction;
	}

	public void StopMove (float input_direction) {
		direction = 0f;
	}

	void OnTriggerEnter2D (Collider2D stopIndicator) {
		if (stopIndicator.CompareTag ("ElevatorStart")) {
			direction = 0f;
			movespeed = Mathf.Abs (movespeed);
			return;
		}

		if (stopIndicator.CompareTag ("ElevatorEnd")) {
			direction = 0f;
			movespeed = -Mathf.Abs (movespeed);
			return;
		}
	}
}
