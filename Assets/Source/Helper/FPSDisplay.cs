﻿using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	private float deltaTime = 0.0f;
	
	private void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
	
	private void OnGUI()
	{
		int 		w 		= Screen.width, h = Screen.height;
		float 		msec 	= deltaTime * 1000.0f;
		float 		fps 	= 1.0f / deltaTime;
		string 		text 	= string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUIStyle 	style 	= new GUIStyle();
		Rect 		rect 	= new Rect(0, 0, w, h * 2 / 100);

		style.alignment 		= TextAnchor.UpperLeft;
		style.fontSize 			= h * 2 / 100;
		style.normal.textColor 	= Color.white;

		GUI.Label(rect, text, style);
	}
}