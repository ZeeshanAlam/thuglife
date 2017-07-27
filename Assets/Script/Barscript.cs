﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barscript : MonoBehaviour {

	private float fillAmount;

	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Image content; 

	[SerializeField]
	private Text valueText;

	public float Maxvalue { get; set; }

	public float Value {
		set {
			string[] temp = valueText.text.Split (':');
			valueText.text = temp [0] + ": " + value; 
			fillAmount = Map (value, 0, Maxvalue, 0, 1);
		}
	}
		

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	private void HandleBar(){
		if (fillAmount != content.fillAmount) {
			content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime*lerpSpeed);
		}
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax){

		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

	}
}
