using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWithDefaultEnemy : Tile {

	void Start () {
		dangerArea = transform.Find ("Danger_area");
		dangerAreaScript = dangerArea.GetComponent<DangerArea> ();
		enemieListScript = GameObject.Find ("TileGenerator").GetComponent<EnemiesList> ();
		GenerateEnemies ();
	}

	//Сгенерировать врагов
	EnemiesList enemieListScript;
	void GenerateEnemies () {
		//Выбрать случайный стак мобов в соответствии с текущей сложностью
		int[] chosenPattern = enemieListScript.ChooseEnemieStackPattern (complexity);

		for (int i = 0; i < chosenPattern.Length; i++) {
			CreateEnemies (enemieListScript.GetRandomEnemieFromTier(chosenPattern[i]));
		}
		dangerAreaScript.FindUnits ();
	}

	Transform dangerArea;
	DangerArea dangerAreaScript;
	//Сгенерировать случайного врага
	void CreateEnemies (GameObject chosenEnemie) {
		float randomEnemiePosition = Random.Range (5f, 15f);
		GameObject enemie = Instantiate (chosenEnemie, new Vector2 (transform.position.x + randomEnemiePosition, transform.position.y + 5f), Quaternion.identity);
		enemie.transform.SetParent (dangerArea);
		Unit enemieScript = enemie.GetComponent<Unit> ();
		enemieScript.RegistrationInStack (dangerAreaScript);
	}
}
