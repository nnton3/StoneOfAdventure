using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector2 startPosition;
	public Vector2 endPosition;

	void Start () {
		tileGenerator = GameObject.Find ("TileGenerator").GetComponent<TileGenerator> ();
		dangerArea = transform.Find ("Danger_area");
		dangerAreaScript = dangerArea.GetComponent<DangerArea> ();
		GenerateEnemies ();
	}

	public int enemiesNumber = 3;

	void GenerateEnemies () {
		for (int i = 0; i < enemiesNumber; i++) {
			CreateEnemies ();
		}
		dangerAreaScript.FindUnits ();
	}

	TileGenerator tileGenerator;
	Transform dangerArea;
	DangerArea dangerAreaScript;

	void CreateEnemies () {
		int randomIntNumber = Random.Range (0, tileGenerator.enemiesSheet.Length);

		GameObject enemie = Instantiate (tileGenerator.enemiesSheet [randomIntNumber], new Vector2 (transform.position.x, transform.position.y + 5f), Quaternion.identity);
		enemie.transform.SetParent (dangerArea);
		Unit enemieScript = enemie.GetComponent<Unit> ();
		enemieScript.RegistrationInStack (dangerAreaScript);
	}
}
