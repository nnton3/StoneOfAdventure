using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

	Rigidbody2D rb;

	public Vector2 directVector;
	public void Impulse (int direction) {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2 (300f * direction, 500f));
	}

	public void DestroyObject () {
		Destroy (gameObject);
	}
}
