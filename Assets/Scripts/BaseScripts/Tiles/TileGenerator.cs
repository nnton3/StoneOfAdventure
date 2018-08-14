using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

	public int TileNumber = 5;
	public GameObject[] tilesSheet;
	public GameObject[] enemiesSheet;
	public Vector2 wayPosition = new Vector2 (0f, 0f);

	void Start () {
		GenerateLevel ();
	}

	void GenerateLevel () {
		for (int i = 0; i < TileNumber; i++) {
			CreateTile ();
		}
	}

	void CreateTile () {
		int randomNumber = Random.Range (0, tilesSheet.Length);

		Tile tileScript = tilesSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (tilesSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);

	}
}
