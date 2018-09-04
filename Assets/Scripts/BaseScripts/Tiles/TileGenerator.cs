using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

	public int TileNumber = 5;
	public GameObject[] specialTilesSheet;
	public Vector2 wayPosition = new Vector2 (0f, 0f);

	void Start () {
		GenerateLevel ();
	}

	void GenerateLevel () {
		//Создать начальный тайл
		CreateStartTile ();
		//Создатьтайлы уровня
		for (int i = 0; i < TileNumber; i++) {
			CreateDefaultTile ();
			CreateTransitionTile ();
		}
		//Создать конечный спрайт
		CreateEndTile ();
	}

	//Создать начальный тайл
	public GameObject startTile;

	void CreateStartTile () {
		Tile tileScript = startTile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (startTile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	//Создать тайл с врагами
	public GameObject[] defaultTilesSheet;

	void CreateDefaultTile () {
		int randomNumber = Random.Range (0, defaultTilesSheet.Length);

		Tile tileScript = defaultTilesSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (defaultTilesSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	//Создать тайл переход
	public GameObject[] transitionTilesSheet;

	void CreateTransitionTile () {
		int randomNumber = Random.Range (0, transitionTilesSheet.Length);

		Tile tileScript = transitionTilesSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (transitionTilesSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	//Создать последний тайл
	public GameObject endTile;

	void CreateEndTile () {
		Tile tileScript = endTile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (endTile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}
}
