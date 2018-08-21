using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	Rigidbody2D rb;
	float direction = 0;
	public float movespeed = 2f;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.velocity = new Vector2 (rb.velocity.x, direction * movespeed);
	}



	public void StartMove () {
		direction = 1;
	}

	public void StopMove (float input_direction) {
		direction = 0f;
	}

	void OnTriggerEnter2D (Collider2D stopIndicator) {
		if (stopIndicator.CompareTag ("Indicator")) {
			direction = 0f;
			movespeed *= -1;
			return;
		}
	}
}
