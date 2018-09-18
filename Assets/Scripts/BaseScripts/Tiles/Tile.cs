using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
\brief Абстрактный класс, являющийся родительским для всех тайлов
		   
		   Он содержит информацию используемую в GameLevel для правильного расположения тайлов на уровне, относительно друг друга	
*/
public class Tile : MonoBehaviour {

	[HideInInspector]
	public Vector2 startPosition;   ///< Координаты точки, в которой находится начало данного тайла, относительно предыдущего тайла
	[HideInInspector]
	public Vector2 endPosition;   ///< Координаты точки, к которой должен быть прикреплен следующий тайл, относительно данного

	public int complexity = 0;   ///< Сложность мобов для данного тайла
}
