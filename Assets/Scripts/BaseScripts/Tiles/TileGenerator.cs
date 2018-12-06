using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Абстрактный класс, используемый генераторами уровня для создания тайлов.
	   
	   Содержит методы для создания конкретных тайлов и создания случайного тайла из списка.
*/
public class TileGenerator : MonoBehaviour {
	
	public Vector2 wayPosition = new Vector2 (0f, 0f);   ///< Координаты создания тайла. Обновляется после создания очередного тайла.

	///Задает конкретное значение для wayPosition. Используется для ручного задания начальной точки создания уровня
	public void SetStartPosition (Vector2 startPosition) {
		wayPosition = startPosition;
	}

	/*! Создание конкретного тайла
		
		\param[in] tile Ссылка на префаб тайла, который необходимо создать
		\param[in] parent Ссылка на родительский объект, в котором в данный момент генерируются тайлы
		\param[in] complexity Сложность мобов, генерируемых на этом тайле
	*/
	public void CreateTile (GameObject tile, Transform parent, int complexity) {
		Tile tileScript = tile.GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		GameObject tileInstance = Instantiate (tile, new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		tileInstance.transform.SetParent (parent);
		Tile tileInstanceScript = tileInstance.GetComponent<Tile>();
		tileInstanceScript.complexity = complexity;
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}

	/*! Создание случайного тайла из списка
		
		\param[in] tileSheet Список доступных тайлов, из которого выбирается один случайным образом
		\param[in] parent Ссылка на родительский объект, в котором в данный момент генерируются тайлы
		\param[in] complexity Сложность мобов, генерируемых на этом тайле
	*/
	public void CreateRandomTile (GameObject[] tileSheet, Transform parent, int complexity) {
		int randomNumber = Random.Range (0, tileSheet.Length);

		Tile tileScript = tileSheet [randomNumber].GetComponent<Tile> ();

		Vector2 start = tileScript.startPosition;
		Vector2 end = tileScript.endPosition;

		GameObject tileInstance = Instantiate (tileSheet [randomNumber], new Vector2 (wayPosition.x + start.x, wayPosition.y + start.y), Quaternion.identity);
		tileInstance.transform.SetParent (parent);
		Tile tileInstanceScript = tileInstance.GetComponent<Tile>();
		tileInstanceScript.complexity = complexity;
		wayPosition = new Vector2 (wayPosition.x + end.x, wayPosition.y + end.y);
	}
}
