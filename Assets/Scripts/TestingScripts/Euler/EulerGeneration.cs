using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerGeneration : MonoBehaviour {

	public int lineNumber;
	int currentLine;
	public int columnNumber;
	int currentColumn = 1;
	public float tileDx;
	public float tileDy;

	GameObject[] lineArray = new GameObject[15];
	void CreateFirstLine () {
		currentLine = 1;
		for (currentColumn = 1; currentColumn <= columnNumber; currentColumn++) {
			lineArray [currentColumn - 1] = CreateTile (defaultTile, new Vector2 (currentColumn, currentLine), currentColumn);
			wayPoint.x += tileDx;
		}
		wayPoint.x = startPoint.x;
	}

	public GameObject leftBound;
	public GameObject topBound;
	public GameObject bottomBound;
	GameObject CreateTile (GameObject tile, Vector2 tilePosition, int arrayNumber) {
		GameObject tileInstance = Instantiate (tile, wayPoint, Quaternion.identity);
		EulerTile tileScript = tileInstance.GetComponent<EulerTile> ();
		tileScript.arrayNumber = arrayNumber;
		tileScript.positionInTable = tilePosition;
		if (tileScript.positionInTable.x == 1) {
			Instantiate (leftBound, tileInstance.transform);
		} else if (tileScript.positionInTable.x == columnNumber) {
			Instantiate (rightBound, tileInstance.transform);
		}
		if (tileScript.positionInTable.y == 1) {
			Instantiate (topBound, tileInstance.transform);
		} else if (tileScript.positionInTable.x == lineNumber) {
			Instantiate (bottomBound, tileInstance.transform);
		}
		return tileInstance;
	}

	public GameObject rightBound;
	void CreateRightBound () {
		for (int i = 1; i < columnNumber; i++) {
			int chance = Random.Range (0, 2);
			EulerTile tileScript = lineArray [i - 1].GetComponent<EulerTile> ();
			EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();
			if (tileScript.arrayNumber == NeighborTileScript.arrayNumber || chance > 0.5) {
				Instantiate (rightBound, lineArray [i - 1].transform);
				tileScript.rightBound = true;
			} else {
				NeighborTileScript.arrayNumber = tileScript.arrayNumber;
			}
		}
	}

	void CreateBottomBound () {
		int timer = 0;
		GameObject[] arrayOfTiles = new GameObject[15];
		for (int i = 1; i < columnNumber; i++) {
			EulerTile tileScript = lineArray [i - 1].GetComponent<EulerTile> ();
			EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();
			if (tileScript.arrayNumber == NeighborTileScript.arrayNumber) {
				Debug.Log ("тайлы из одного множества, записать тайл в массив");
				arrayOfTiles [timer] = lineArray [i - 1];
				timer++;
			} else {
				Debug.Log ("тайлы не из одного множества, записать тайл в массив и выбрать тайлы с нижней границей");
				arrayOfTiles [timer] = lineArray [i - 1];
				Debug.Log (arrayOfTiles [1]);
				if (arrayOfTiles [1] != null) {
					int numberOfTilesWithBottomBound = 0;
					for (int j = 0; j < arrayOfTiles.Length; j++) {
						if (arrayOfTiles [j] != null) {
							int chance = Random.Range (0, 2);
							if (chance > 0.5) {
								Debug.Log ("Добавить нижнюю границу тайлу " + lineArray [j].GetComponent<EulerTile> ().positionInTable.x);
								arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound = true;
								numberOfTilesWithBottomBound++;
							}
						}
					}

					if (numberOfTilesWithBottomBound == arrayOfTiles.Length) {
						arrayOfTiles [0].GetComponent<EulerTile> ().bottomBound = false;
					}

					for (int j = 0; j < arrayOfTiles.Length; j++) {
						if (arrayOfTiles [j] != null && arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound == true) {
							Instantiate (bottomBound, arrayOfTiles [j].transform);
						}
					}

					//Очистка массива
					for (int j = 0; j < arrayOfTiles.Length; j++) {
						arrayOfTiles [j] = null;
					}
					timer = 0;
				}
			}
		}
	}

	void Start () {
		CreateFirstLine ();
		CreateRightBound ();
		CreateBottomBound ();
		//LevelGenerate ();
	}

	Vector2 wayPoint = new Vector2 ();
	public GameObject defaultTile;
	public Vector2 startPoint;
	/*public int lines;
	void LevelGenerate () {
		wayPoint = startPoint;
		for (int i = 0; i <= lines; i++ ) {
			GenerateLine ();
		}
	}

	public float tileDx;
	public float tileDy;
	public int columns;
	void GenerateLine () {
		wayPoint.x = startPoint.x;
		for (int j = 0; j <= columns; j++) {
			lineTileArray[j] = CreateTile (defaultTile);
			wayPoint.x += tileDx;
		}
		CreateRightBounds (lineTileArray);
		wayPoint.y -= tileDy;
	}

	GameObject[] lineTileArray = new GameObject[5];
	GameObject CreateTile (GameObject tile) {
		GameObject tileInstance = Instantiate (tile, wayPoint, Quaternion.identity);
		return tileInstance;
	}

	public GameObject rightBound;
	void CreateRightBounds (GameObject[] lineTileArray) {
		List<Transform> groupOfTiles = new List<Transform> ();
		for (int k = 0; k <= (lineTileArray.Length - 1); k++) {
			float chance = Random.Range (0, 2);
			if (chance > 0.5f) {
				Instantiate (rightBound, lineTileArray [k].transform);
				groupOfTiles.Add (lineTileArray [k].transform);
				groupOfTiles.TrimExcess ();
				CreateBottomBound (groupOfTiles);
				groupOfTiles.Clear ();
			} else {
				groupOfTiles.Add (lineTileArray [k].transform);
			}
		}
	}

	public GameObject bottomBound;
	void CreateBottomBound (List<Transform> groupOfTiles) {
		if (groupOfTiles.Capacity != 1) {
			int chance = Random.Range (1, (groupOfTiles.Capacity + 1));
			Debug.Log ("Номер платформы без нижней границы: " + chance);
			int timer = 1;
			foreach (Transform tileObject in groupOfTiles) {
				if (timer != chance) {
					Instantiate (bottomBound, tileObject);
				}
				timer++;
			}
		}
	}*/
}
