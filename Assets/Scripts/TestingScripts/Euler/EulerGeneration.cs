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

	void Start () {
		
		CreateFirstLine ();
		CreateRightBound ();
		CreateBottomBoundsWithinOneLine ();
		wayPoint.x = startPoint.x;
		wayPoint.y -= tileDy;
		StartCoroutine ("CreateDefaultLine");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			FindTileNoArrayNumber ();
		}
	}

	void CreateLine () {
		for (int i = 0; i < lineArray.Length; i++) {
			GameObject tileInstance = Instantiate (lineArray [i], wayPoint, Quaternion.identity);
			DeleteTopBound (tileInstance);
			DeleteRightBound (tileInstance);
			workingArray [i] = tileInstance;
			wayPoint.x += tileDx;
		}

		wayPoint.x = startPoint.x;
		wayPoint.y -= tileDy;
	}
		
	void ClearArray () {
		ClearArrayForNextIteration (lineArray);
		workingArray.CopyTo (lineArray, 0);
		ClearArrayForNextIteration (workingArray);
	}

	public GameObject[] lineArray = new GameObject[15];
	void CreateFirstLine () {
		currentLine = 1;
		for (currentColumn = 1; currentColumn <= columnNumber; currentColumn++) {
			lineArray [currentColumn - 1] = CreateTile (defaultTile, new Vector2 (currentColumn, currentLine), currentColumn);
			wayPoint.x += tileDx;
		}
		wayPoint.x = startPoint.x;
	}

	public GameObject[] workingArray = new GameObject[15];
	IEnumerator CreateDefaultLine () {
		for (currentLine = 2; currentLine <= lineNumber; currentLine++) {
			if (currentLine == lineNumber) {
				CreateLastLine ();
			} else {
				CreateLine ();
				yield return new WaitForSeconds (0.1f);
				ClearArray ();
				DeleteTilesWithBottomBoundFromArrays ();
				FindTileNoArrayNumber ();
				for (int i = 0; i < lineArray.Length; i++) {
					yield return new WaitForSeconds (0.1f);
					DeleteBottomBound (lineArray [i]);
				}
				CreateRightBound ();
				CreateBottomBoundsWithinOneLine ();
			}
		}
	}

	void CreateLastLine() {
		for (int i = 0; i < columnNumber; i++) {
			GameObject tileInstance = Instantiate (lineArray [i], wayPoint, Quaternion.identity);
			workingArray [i] = tileInstance;
			wayPoint.x += tileDx;
		}
		ClearArray ();
		CreateBottomBound ();
		for (int i = 1; i < columnNumber; i++) {
			EulerTile tileScript = lineArray [i-1].GetComponent<EulerTile> ();
			EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();
			if (tileScript.arrayNumber != NeighborTileScript.arrayNumber) {
				DeleteRightBound (lineArray [i - 1]);
				NeighborTileScript.arrayNumber = tileScript.arrayNumber;
				Debug.Log ("Граница тайла " + lineArray [i - 1].GetComponent<EulerTile>().positionInTable.x + " разрушена");
			}
		}
	}

	void DeleteTopBound (GameObject currentTile) {
		EulerTile tileScript = currentTile.GetComponent<EulerTile> ();
		if (tileScript.topBound) {
			GameObject topBoundInstance = currentTile.transform.Find ("Tile_top_platform(Clone)").gameObject;
			Destroy (topBoundInstance);
			currentTile.GetComponent<EulerTile> ().topBound = false;
		}
	}

	void DeleteRightBound (GameObject currentTile) {
		EulerTile tileScript = currentTile.GetComponent<EulerTile> ();
		if (tileScript.rightBound && tileScript.positionInTable.x != columnNumber) {
			GameObject rightBoundInstance = currentTile.transform.Find ("Tile_right_platform(Clone)").gameObject;
			Destroy (rightBoundInstance);
			tileScript.rightBound = false;
		}
	}

	void DeleteBottomBound (GameObject currentTile) {
		EulerTile tileScript = currentTile.GetComponent<EulerTile> ();
		if (tileScript.bottomBound) {
			GameObject bottomBoundInstance = currentTile.transform.Find ("Tile_bottom_platform(Clone)").gameObject;
			Destroy (bottomBoundInstance);
			tileScript.bottomBound = false;
		}
	}

	void DeleteTilesWithBottomBoundFromArrays () {
		for (int i = 0; i < lineArray.Length; i++) {
			EulerTile tileScript = lineArray[i].GetComponent<EulerTile> ();
			if (tileScript.bottomBound == true) {
				tileScript.arrayNumber = 0;
			}
		}
	}

	void FindTileNoArrayNumber () {
		for (int i = 0; i < lineArray.Length; i++) {
			EulerTile tileScript = lineArray [i].GetComponent<EulerTile> ();
			if (tileScript.arrayNumber == 0) {
				tileScript.arrayNumber = GiveNewArrayNumber ();
			}
		}
	}

	int GiveNewArrayNumber () {
		int freeArrayNumber = 1;
		bool coincidence = false;
		for (int j = 0; j < lineArray.Length; j++) {
			for (int i = 0; i < lineArray.Length; i++) {
				if (lineArray [i].GetComponent<EulerTile> ().arrayNumber == freeArrayNumber) {
					coincidence = true;
				}
			}
			if (coincidence) {
				freeArrayNumber++;
				coincidence = false;
			} else {
				return freeArrayNumber;
			}
		}
		return 0;
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
			tileInstance.GetComponent<EulerTile> ().leftBound = true;
		} else if (tileScript.positionInTable.x == columnNumber) {
			Instantiate (rightBound, tileInstance.transform);
			tileInstance.GetComponent<EulerTile> ().rightBound = true;
		}
		if (tileScript.positionInTable.y == 1) {
			Instantiate (topBound, tileInstance.transform);
			tileInstance.GetComponent<EulerTile> ().topBound = true;
		} else if (tileScript.positionInTable.x == lineNumber) {
			Instantiate (bottomBound, tileInstance.transform);
			tileInstance.GetComponent<EulerTile> ().bottomBound = true;
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
		for (int i = 0; i < columnNumber; i++) {
			EulerTile tileScript = lineArray [i].GetComponent<EulerTile> ();
			if (!tileScript.bottomBound) {
				Instantiate (bottomBound, lineArray [i].transform);
				tileScript.bottomBound = true;
			}
		}
	}

	void CreateBottomBoundsWithinOneLine () {
		int timer = 0;
		GameObject[] arrayOfTiles = new GameObject[15];
		for (int i = 1; i <= columnNumber; i++) {
			EulerTile tileScript = lineArray [i - 1].GetComponent<EulerTile> ();
			if (LastColumns (i)) {
				CreateBottomBoundsWithinOneArray (arrayOfTiles, timer, i);
				timer = 0;
			} else {
				EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();
				if (TilesNotFromOneArray (tileScript, NeighborTileScript)) {
					CreateBottomBoundsWithinOneArray (arrayOfTiles, timer, i);
					timer = 0;
					continue;
				} else {
					arrayOfTiles [timer] = lineArray [i - 1];
					timer++;
					continue;
				}
			}
		}
	}

	void CreateBottomBoundsWithinOneArray (GameObject[] arrayOfTiles, int tilesNumberInOneArray, int NumberOfTileInLine) {
		arrayOfTiles [tilesNumberInOneArray] = lineArray [NumberOfTileInLine - 1];
		//Если в множестве более одного тайла
		if (NullCheck(arrayOfTiles [1])) {
			int numberOfTilesWithBottomBound = 0;   ///< Счетчик тайлов, имеющих нижнюю границу
			int tilesInArray = 0;   ///< Счетчик тайлов в пределах одного множества

			for (int j = 0; j < arrayOfTiles.Length; j++) {
				if (NullCheck(arrayOfTiles [j])) {
					tilesInArray++;
					int chance = Random.Range (0, 2);   ///< От этого значения зависит будем создавать нижнюю границу или нет. Если больше 0.5 то создаем, если меньше, то нет
					if (chance > 0.5) {
						arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound = true;
						numberOfTilesWithBottomBound++;
					}
				}
			}

			if (numberOfTilesWithBottomBound == tilesInArray) {
				arrayOfTiles [0].GetComponent<EulerTile> ().bottomBound = false;
			}

			for (int j = 0; j < arrayOfTiles.Length; j++) {
				if (arrayOfTiles [j] != null && arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound == true) {
					Instantiate (bottomBound, arrayOfTiles [j].transform);
				}
			}
			ClearArrayForNextIteration (arrayOfTiles);
		}
	}
	///Проверка: тайл я вляется последним в строке?
	bool LastColumns (int tileNumber) {
		if (tileNumber == columnNumber) {
			return true;
		} else
			return false;
	}
	///Проверка:  принадлежат ли тайлы к одному множеству
	bool TilesNotFromOneArray (EulerTile firstTile, EulerTile secondTile) {
		if (firstTile.arrayNumber != secondTile.arrayNumber) {
			return true;
		} else
			return false;
	}
	///Очистить массив для использования в проверке следующего множества
	void ClearArrayForNextIteration (GameObject[] arrayOfTiles) {
		for (int j = 0; j < arrayOfTiles.Length; j++) {
			arrayOfTiles [j] = null;
		}
	}

	bool NullCheck (GameObject tile) {
		if (tile != null) {
			return true;
		} else
			return false;
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
