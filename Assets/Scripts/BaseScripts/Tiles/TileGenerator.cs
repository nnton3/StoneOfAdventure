using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {
	
	//Ориентир для следующего тайла в очереди создания обычных тайлов
	public Vector2 wayPosition = new Vector2 (0f, 0f);

	//Задать начальную точку
	public void SetStartPosition (Vector2 startPosition) {
		wayPosition = startPosition;
	}

	//Создать конкретный тайл
	public void CreateTile (GameObject tile, Transform parent) {
		Tile tileScript = tile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (tile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity).transform.SetParent(parent);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	//Создать случайный тайл из массива
	public void CreateRandomTile (GameObject[] tileSheet, Transform parent) {
		int randomNumber = Random.Range (0, tileSheet.Length);

		Tile tileScript = tileSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (tileSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity).transform.SetParent(parent);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}
}
