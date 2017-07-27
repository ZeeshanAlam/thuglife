using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour {

	public int moveSpeed=-5;
	public bool die=true;
	public float deathTime=1;
	public Transform target;
	public Transform ThunderPrefab;
	private bool reSpawn=false;
	public GameObject Thunder;

	private GameObject thunder;

	void Start(){

		StartCoroutine (delay ());
		
	}

	void Update () {
		
	}


		IEnumerator delay(){
			while(true)
			{
			if (die)
				Destroy (gameObject, deathTime);

			if ((target.position.x <= transform.position.x + 1) && (target.position.x >= transform.position.x - 1)) {

				transform.Translate (Vector3.right * Time.deltaTime * moveSpeed / 2);
				thunder = GameObject.Instantiate (Thunder);
				thunder.transform.SetParent (transform);
				thunder.transform.position = transform.position;
				thunder.SetActive (true);
				yield return new WaitForSeconds (3);
				
			} else if (target.position.x >= transform.position.x + 1) {
					
				transform.Translate (Vector3.right * Time.deltaTime * moveSpeed * 10);
				yield return new WaitForSeconds (0.01f);
			}
			else {
				transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
					yield return new WaitForSeconds (0.01f);
			}
		
		}
	}
}
