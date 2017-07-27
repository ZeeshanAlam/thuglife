using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

	public int moveSpeed=230;
	public bool die=true;
	public float deathTime=1;

	void Update () {
//		if (player.transform.localScale.x > 0) {
//			transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
//		} else {
//			transform.Translate (Vector3.left * Time.deltaTime * moveSpeed);
//		}
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		if (die)
		Destroy (gameObject, deathTime);
	}
}
