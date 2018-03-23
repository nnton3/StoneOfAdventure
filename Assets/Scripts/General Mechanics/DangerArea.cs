using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArea : MonoBehaviour {

	alert enemieAlert;
	idle enemieIdle;
	List<IReaction<GameObject>> enemies = new List<IReaction<GameObject>>();

	void Start () {
		foreach (IReaction<GameObject> enemy in enemies) {
			enemieAlert += enemy.Chase;
		}

		foreach (IReaction<GameObject> enemy in enemies) {
			enemieIdle += enemy.Idle;
		}
	}

	public void AddEnemie(IReaction<GameObject> newEnemie) {
		enemies.Add (newEnemie);
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("work");
		if (other.CompareTag ("Player")) {
			enemieAlert (other.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			enemieIdle ();
		}
	}
}
