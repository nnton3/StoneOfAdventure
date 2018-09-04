using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {


	void Start () {
		GenerateLevel ();
	}

	//Сколько всего будет тайлов без учета тайлов-переходом
	public int TileNumber = 5;
	void GenerateLevel () {
		//Создать начальный тайл
		CreateStartTile ();
		//Создатьтайлы уровня
		for (int i = 0; i < TileNumber; i++) {
			//Сгенерировать тайл для i-ой позиции
			SelectTile (i);
			CreateTransitionTile ();
		}
		//Создать конечный спрайт
		CreateEndTile ();
	}
		
	//Проверка: позиция зарезервирована под спец тайл или нет
	void SelectTile (int position) {
		for (int i = 0; i < specialTilesSheet.Length; i++) {
			TileCrossroads tileScript = specialTilesSheet [i].GetComponent<TileCrossroads> ();
			//Если спец тайл должен находиться на этой позиции
			if (position != tileScript.tilePositionOnLevel) {
				//Сгенерировать его
				CreateSpecialTile ();
			} else
				//Иначе сгенерировать тайл с врагами
				CreateDefaultTile ();
		}
	}

	//Ориентир для следующего тайла в очереди создания обычных тайлов
	public Vector2 wayPosition = new Vector2 (0f, 0f);

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

	//Создать спец тайл
	public GameObject[] specialTilesSheet;

	void CreateSpecialTile () {
		int randomNumber = Random.Range (0, specialTilesSheet.Length);

		Tile tileScript = specialTilesSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		Instantiate (specialTilesSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
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
