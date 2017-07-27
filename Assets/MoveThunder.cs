using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThunder : MonoBehaviour {

	public int moveSpeed=2;
	public Transform target;

	void Update () {

		if (target == null)
			Debug.Log ("no witch");

		float angle = Mathf.Atan2(target.position.y - transform.parent.position.y, target.position.x - transform.parent.position.x)
			+ Mathf.PI;
		transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);


		transform.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(-moveSpeed, 0.0f);
		
	}
}
