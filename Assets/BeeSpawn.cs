using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawn : MonoBehaviour {

	private Transform bee;
	// Use this for initialization
	void Start () {
		bee = transform.Find ("Bee");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if (bee == null)
				Debug.Log ("OO");

			transform.GetChild (0).gameObject.SetActive (true);
		}

	}

}
