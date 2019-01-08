using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponentInParent<Player> ();
	}

	void OnTriggerEnter2D (Collider2D enemy) {
		if (enemy.CompareTag("Enemy")) {
            Debug.Log("Set damage");
			enemy.GetComponent<Damage> ().DefaultDamage (player.attackPoints, player.direction);
		}
	}
}
