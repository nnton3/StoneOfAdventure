using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow: Unit {

	/*public AudioClip audioFly;
	bool audioFlyCheck = true;


	public Vector3 velocity;
	public float moveSpeed;
	Controller2D controller;
	float gameTime = 1f;
	float _direction;

	void Awake () {
		StartCoroutine ("Pause");
	}

	void Start () {
		controller = GetComponent<Controller2D> ();
	}

	void Update() {
	
		if (gameTime != Time.timeScale) {
			velocity.x = Vector2.right.x * moveSpeed * _direction * Time.timeScale;
			gameTime = Time.timeScale;
		}

		controller.Move (velocity);

		if (velocity.x > 0 && audioFlyCheck) {
			audioFlyCheck = false;
			StartCoroutine ("AudioFly");
		}
	}

	IEnumerator Pause() {
		yield return new WaitForSeconds (5f);
		Destroy (gameObject);
	}

	public void SetCaster(Archer archer, float direction) {
		_direction = direction;
		velocity.x = Vector2.left.x * moveSpeed * _direction;
	}

	IEnumerator AudioFly () {
		//audioController.PlayOneShot (audioFly, .5f);
		yield return new WaitForSeconds (.3f);
		audioFlyCheck = true;
	}*/
	public override void GetDamage (){}

	public override void SetDamage (float damage){}

	public override void SetStun (){}

	public override void Die (){}
}
