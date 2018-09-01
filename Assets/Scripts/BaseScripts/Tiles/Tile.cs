using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector2 startPosition;
	public Vector2 endPosition;

	void Start () {
		//Если тайл стартовый - ничего не делать
		if (startTile || endTile || transitionTile) {
			return;
		}
		dangerArea = transform.Find ("Danger_area");
		dangerAreaScript = dangerArea.GetComponent<DangerArea> ();
		enemieListScript = GameObject.Find ("TileGenerator").GetComponent<EnemiesList> ();
		GenerateEnemies ();
	}

	//Тайл обычный или нет?
	public bool startTile;
	public bool endTile;
	public bool transitionTile;

	bool DefaultTile () {
		if (startTile || endTile || transitionTile) {
			return false;
		} else
			return true;
	}

	//Сгенерировать врагов
	EnemiesList enemieListScript;
	void GenerateEnemies () {
		//Выбрать случайный стак мобов в соответствии с текущей сложностью
		int[] chosenPattern = enemieListScript.ChooseEnemieStackPattern ();

		for (int i = 0; i < chosenPattern.Length; i++) {
			CreateEnemies (enemieListScript.GetRandomEnemieFromTier(chosenPattern[i]));
		}
		dangerAreaScript.FindUnits ();
	}

	Transform dangerArea;
	DangerArea dangerAreaScript;

	void CreateEnemies (GameObject chosenEnemie) {
		float randomEnemiePosition = Random.Range (-5f, 5f);
		GameObject enemie = Instantiate (chosenEnemie, new Vector2 (transform.position.x + randomEnemiePosition, transform.position.y + 5f), Quaternion.identity);
		enemie.transform.SetParent (dangerArea);
		Unit enemieScript = enemie.GetComponent<Unit> ();
		enemieScript.RegistrationInStack (dangerAreaScript);
	}
}
