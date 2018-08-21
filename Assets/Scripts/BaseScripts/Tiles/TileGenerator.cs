using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

	public int TileNumber = 5;
	public GameObject[] tilesSheet;
	public GameObject startTile;
	public GameObject endTile;
	public Vector2 wayPosition = new Vector2 (0f, 0f);

	void Start () {
		GenerateLevel ();
	}

	void GenerateLevel () {
		for (int i = 0; i < TileNumber; i++) {
			if (i != 0 && i != (TileNumber - 1)) {
				CreateTile ();
				continue;
			} else if (i == 0) {
				CreateStartTile ();
				continue;
			} else if (i == (TileNumber - 1)) {
				CreateEndTile ();
				continue;
			}
		}
	}

	//Создать начальный тайл
	void CreateStartTile () {
		Tile tileScript = startTile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (startTile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	//Создать случайный тайл
	void CreateTile () {
		int randomNumber = Random.Range (0, tilesSheet.Length);

		Tile tileScript = tilesSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (tilesSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);

	}

	//Создать последний тайл
	void CreateEndTile () {
		Tile tileScript = endTile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (endTile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}
}
