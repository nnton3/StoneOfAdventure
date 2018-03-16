﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThrustProgressBar : MonoBehaviour {

	[Header("OBJECTS")]
	public Transform loadingBar;

	[Header("VARIABLES (IN-GAME)")]
	public bool isOn;
	[Range(0, 100)] public float currentPercent;
	[Range(0, 100)] public int speed;

	void Update ()
	{
		if (currentPercent > 0 && isOn == true) 
		{
			currentPercent -= speed * Time.deltaTime;
		}
		loadingBar.GetComponent<Image> ().fillAmount = currentPercent / 100;
	}

	public void GoCD() {
		currentPercent = 100;
	}
}
