using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	public Transform player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{

			Debug.Break ();
			Debug.Log ("hau");
		}
		if (other.gameObject.transform.parent) {
			Destroy (other.gameObject.transform.parent.gameObject);

		} else {		
			Destroy (other.gameObject);
		}

	}

	void Update(){
		Vector3 newPos = new Vector3 (player.position.x, transform.position.y);
		transform.position = newPos;

	}
}
