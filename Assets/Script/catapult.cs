using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catapult : MonoBehaviour {

	GameObject prefab;

	void Start () {
		prefab = Resources.Load ("Gulel") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			GameObject projectile = Instantiate (prefab) as GameObject;
		}

	}
}
