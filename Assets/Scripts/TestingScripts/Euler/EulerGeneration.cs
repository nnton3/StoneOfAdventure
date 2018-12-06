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

	//СОЗДАНИЕ ПЕРВОЙ СТРОКИ
	public GameObject[] lineArray = new GameObject[15];
	void CreateFirstLine () {
		currentLine = 1;
		for (currentColumn = 1; currentColumn <= columnNumber; currentColumn++) {
			lineArray [currentColumn - 1] = CreateTile (defaultTile, new Vector2 (currentColumn, currentLine), currentColumn);
			wayPoint.x += tileDx;
		}
		CreateRightBound ();
		CreateBottomBoundsWithinOneLine ();
		wayPoint.x = startPoint.x;
		wayPoint.y -= tileDy;
	}


	public GameObject leftBound;
	public GameObject topBound;
	public GameObject bottomBound;

	///Создание тайла с учетом его позиции на уровне и номером множества к которому он принадлежит
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
	///Правило создания правой границы тайла
	void CreateRightBound () {
		//Применяется ко всем тайлам, кроме крайнего правого
		for (int i = 1; i < columnNumber; i++) {
			int chance = Random.Range (0, 2);   ///< Принимаем решение создавать границу или нет
			EulerTile tileScript = lineArray [i - 1].GetComponent<EulerTile> ();   ///< Ссылка на текущий тайл
			EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();   ///< Ссылка на соседний справа тайл
			//Если тайлы принадлежат к одному множеству
			if (tileScript.arrayNumber == NeighborTileScript.arrayNumber) {
				//Создать правую границу
				Instantiate (rightBound, lineArray [i - 1].transform);
				tileScript.rightBound = true;
			} else {
				if (chance > 0.5) {
					//Создать правую границу
					Instantiate (rightBound, lineArray [i - 1].transform);
					tileScript.rightBound = true;
				} else
					NeighborTileScript.arrayNumber = tileScript.arrayNumber;
			}
		}
	}

	///Создание нижней границы
	void CreateBottomBoundsWithinOneLine () {
		int tilesNumberInOneArray = 0;   //Счетчик тайлов в одном множестве
		GameObject[] arrayOfTiles = new GameObject[15];   // Отдельное множество тайлов
		//Просматриваем все тайлы в строке поочереди
		for (int i = 1; i <= columnNumber; i++) {
			EulerTile tileScript = lineArray [i - 1].GetComponent<EulerTile> ();
			tilesNumberInOneArray++;   //Увеличить счетчик количества тайлов в множестве
			//Если тайл является последним в строке
			if (LastColumns (i)) {
				//Если тайл входит в множество, содержащее больше одного тайла
				if (tilesNumberInOneArray > 1) {
					//Случайным образом создать нижние границы тайлам множества
					arrayOfTiles [tilesNumberInOneArray] = lineArray [i - 1];
					CreateBottomBoundsWithinOneArray (arrayOfTiles, tilesNumberInOneArray, i);
					tilesNumberInOneArray = 0;   // Обнулить счетчик тайлов
				} else
					Debug.Log ("Не добавлять границу, т.к. в множестве всего 1 элемент");
				//Если тайл не является последним в строке
			} else {
				EulerTile NeighborTileScript = lineArray [i].GetComponent<EulerTile> ();
				//Если следующий за этим тайл принадлежит к другому множеству
				if (TilesNotFromOneArray (tileScript, NeighborTileScript)) {
					//Если тайл входит в множество, содержащее больше одного тайла
					if (tilesNumberInOneArray > 1) {
						//Случайным образом создать нижние границы тайлам множества
						arrayOfTiles [tilesNumberInOneArray] = lineArray [i - 1];
						CreateBottomBoundsWithinOneArray (arrayOfTiles, tilesNumberInOneArray, i);
						tilesNumberInOneArray = 0;   // Обнулить счетчик тайлов
					} else
						Debug.Log ("Не добавлять границу, т.к. в множестве всего 1 элемент");
					//Если соседний тайл принадлежит к тому же множеству, что и текущий
				} else {
					Debug.Log ("Это не последний тайл в множестве");
					//Добавить текущий тайл к текущему множеству
					arrayOfTiles [tilesNumberInOneArray] = lineArray [i - 1];
				}
			}
		}
	}

	void CreateBottomBoundsWithinOneArray (GameObject[] arrayOfTiles, int tilesNumberInOneArray, int NumberOfTileInLine) {
		arrayOfTiles [tilesNumberInOneArray] = lineArray [NumberOfTileInLine - 1];
		int numberOfTilesWithBottomBound = 0;   // Счетчик тайлов, имеющих нижнюю границу
		//Решаем каким тайлам одного множества создать нижнюю границу
		for (int j = 0; j < tilesNumberInOneArray; j++) {
			//Если элемент множества не нулевой
			if (NullCheck (arrayOfTiles [j])) {
				int chance = Random.Range (0, 100);   ///< От этого значения зависит будем создавать нижнюю границу или нет. Если больше 0.5 то создаем, если меньше, то нет
				//Если выпал шанс на создание нижней границы
				if (chance > 10) {
					arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound = true;   //Занести тайл в список тайлов, которым будет создана нижняя граница
					numberOfTilesWithBottomBound++;   //Увеличить счетчик тайлов с нижней границей
				}
			}
		}
		Debug.Log ("Число тайлов с нижней границей " + numberOfTilesWithBottomBound + ". Число тайлов в множестве " + tilesNumberInOneArray);
		//Если все тайлы множества находятся в списке на добавление нижней границы
		if (numberOfTilesWithBottomBound == tilesNumberInOneArray) {
			Debug.Log ("Убрать нижнбъюю границу у первого элемента");
			arrayOfTiles [0].GetComponent<EulerTile> ().bottomBound = false;   //Убрать первый тайл множества из списка и не добавлят ему нижнюю границу
		}
		//Добавить нижнюю границу тайлам множества, которым выпал шанс
		for (int j = 0; j < tilesNumberInOneArray; j++) {
			if (arrayOfTiles [j] != null && arrayOfTiles [j].GetComponent<EulerTile> ().bottomBound == true) {
				Debug.Log ("Добавить нижнюю границу " + arrayOfTiles [j].GetComponent<EulerTile> ().positionInTable.x + " тайлу множества");
				Instantiate (bottomBound, arrayOfTiles [j].transform);
			}
		}
		//Очистить массив, содержащий текущее множество тайлов, для последующего использования
		ClearArrayForNextIteration (arrayOfTiles);
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
				yield return new WaitForSeconds (2f);
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
			Debug.Log ("Удалена нижняя граница тайла " + tileScript.positionInTable.x);
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
		
	void CreateBottomBound () {
		for (int i = 0; i < columnNumber; i++) {
			EulerTile tileScript = lineArray [i].GetComponent<EulerTile> ();
			if (!tileScript.bottomBound) {
				Instantiate (bottomBound, lineArray [i].transform);
				tileScript.bottomBound = true;
			}
		}
	}

	///Проверка: тайл является последним в строке?
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
