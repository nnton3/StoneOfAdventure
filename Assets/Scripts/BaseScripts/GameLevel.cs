using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Класс, отвечающий за создание игрового уровня

	   Запускает процесс генерации тайлов, генерирует спец тайлы (квестовые, тайлы-перекрестки и др.). Можно задать динаически изменяемую сложность врагов.
*/
public class GameLevel : MonoBehaviour {

	TileGenerator tileGeneratorInstance = new TileGenerator();   ///< Экземпляр TileGenerator, используемый для создания тайлов
	public Transform parentObject;   ///< Ссылка на местоположение родительского объекта, в котором генерируется уровень

	void Start () {
		tileGeneratorInstance.SetStartPosition (transform.position);
		SetStartComplexity ();
		CalculatePeriodToIncreaseComplexity ();
		GenerateLevel ();
	}

	public int tileNumber = 5;   ///< Величина уровня в тайлах (сколько тайлов на уровне)
	public virtual void GenerateLevel () {
		//Создать стартовый тайл
		CreateStartTile ();
		//Создатьтайлы уровня
		for (int i = 0; i < tileNumber; i++) {
			//Задать сложность мобов
			SetComplexity ();
			Debug.Log ("complexity = " + GameManager.complexity);
			//Сгенерировать тайл для i-ой позиции
			SelectTile (i);
			CreateTransitionTile ();
		}
		//Создать конечный спрайт
		CreateEndTile ();
	}

	//Распределение сложности мобов по уровню
	int currentComplexity;   ///< Текущая сложность на генерируемом уровне
	public int minComplexity;   ///< Минимальная сложность на генерируемом уровне
	public int maxComplexity;   ///< Максимальная сложность на генерируемом уровне
	public int periodToIncreaseComplexity; ///< Интервал (в тайлах) увеличения сложности
	/// Метод вычисляет количество тайлов с одинаковой сложностью, после генерации которых будет произведено увеличение сложности
	void CalculatePeriodToIncreaseComplexity () {
		if ((maxComplexity - minComplexity) != 0) {
			periodToIncreaseComplexity = tileNumber / (maxComplexity - minComplexity);
			tilesToIncreaseComplexity = periodToIncreaseComplexity;
		} else
			periodToIncreaseComplexity = 500;
	}
	/// Задать начальную сложность мобов на уровне
	void SetStartComplexity () {
		while (GameManager.complexity != minComplexity) {
			currentComplexity += 1;
		}
	}

	int tilesToIncreaseComplexity;   ///< Количество тайлов, которые осталось сгенерировать до увеличения сложности
	void SetComplexity () {
		if (tilesToIncreaseComplexity != 0) {
			tilesToIncreaseComplexity -= 1;
		} else {
			currentComplexity += 1;
			tilesToIncreaseComplexity = periodToIncreaseComplexity;
		}
	}

	///Проверка: позиция зарезервирована под спец тайл или нет
	public virtual void SelectTile (int position) {
		//Если спец тайлов нет
		if (specialTilesSheet.Length == 0) {
			CreateDefaultTile ();
			return;
		}
		for (int tileNumber = 0; tileNumber < specialTilesSheet.Length; tileNumber++) {
			TileCrossroads tileScript = specialTilesSheet [tileNumber].GetComponent<TileCrossroads> ();
			Debug.Log ("spec pos = " + tileScript.tilePositionOnLevel + " and tile pos = " + position);
			//Если спец тайл должен находиться на этой позиции
			if (position == tileScript.tilePositionOnLevel) {
				CreateSpecialTile (tileNumber);
				Debug.Log ("special create");
				return;
			} 
		}
		//Иначе сгенерировать тайл с врагами
		CreateDefaultTile ();
		Debug.Log ("default create");
		return;
	}

	public GameObject startTile;   ///< Ссылка на стартовый тайл
	///Создание стартового тайла
	public virtual void CreateStartTile () {
		if (startTile != null) {
			tileGeneratorInstance.CreateTile (startTile, parentObject);
		}
	}

	public GameObject[] defaultTilesSheet;   ///< Ссылка на стандартный тайл с врагами
	///Создание стандартного тайла с врагами
	public virtual void CreateDefaultTile () {
		if (defaultTilesSheet [0] != null) {
			tileGeneratorInstance.CreateRandomTile (defaultTilesSheet, parentObject);
		}
	}

	public GameObject[] specialTilesSheet;   ///< Список специальных тайлов (тайлы-перекрестки, квестовые и др.)
	///Создать спец тайл
	public virtual void CreateSpecialTile (int tileNumber) {
		if (specialTilesSheet [0] != null) {
			tileGeneratorInstance.CreateTile (specialTilesSheet[tileNumber], parentObject);
		}
	}

	///Создать тайл переход
	public GameObject[] transitionTilesSheet;

	public virtual void CreateTransitionTile () {
		if (transitionTilesSheet[0] != null) {
			tileGeneratorInstance.CreateRandomTile (transitionTilesSheet, parentObject);
		}
	}

	public GameObject endTile;   ///< Ссылка на конечный тайл
	///Создание конечного тайла
	public virtual void CreateEndTile () {
		if (endTile != null) {
			tileGeneratorInstance.CreateTile (endTile, parentObject);
		}
	}
}
