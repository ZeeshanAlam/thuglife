﻿using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class Stat  {

	[SerializeField]
	private Barscript bar;
	[SerializeField]
	private float maxVal;
	[SerializeField]
	private float currentVal;

	public float CurrentVal {
		get {
			return currentVal;

		}
		set {
			this.currentVal = Mathf.Clamp( value,0,MaxVal);
			bar.Value = currentVal;
		}
	}


	private float MaxVal {
		get {
			return maxVal;

		}
		set {
			this.maxVal = value;
			bar.Maxvalue = maxVal;
		}
	}

	public void Initialize(){
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}

}
