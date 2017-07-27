using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	[SerializeField]
	public Stat health;

	public float damgeBee=10;
	public float damageWitch=15;
	public float damageGroundEnemy = 10;
	public float coins;

	[SerializeField]
	private Text coinText;

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Bee"){
			health.CurrentVal -= damgeBee;
			if (health.CurrentVal <= 0) {
				GameMaster.KillPlayer (this);
			}
			Vector3 boost = new Vector3 (col.gameObject.transform.position.x +Random.Range(5f,14f), col.gameObject.transform.position.y+Random.Range(3f,10f),0f);
//			if (boost.x < transform.position.x)
//				col.gameObject.transform.RotateAround (boost, transform.up, 180f); 

			col.gameObject.transform.position = boost;
		}
		if (col.gameObject.tag == "Thunder"){
			health.CurrentVal -= damageWitch;
			if (health.CurrentVal <= 0) {
				GameMaster.KillPlayer (this);
			}
			Destroy (col.gameObject, 0.5f);

		}

		if (col.gameObject.tag == "GroundEnemy"){
			health.CurrentVal -= damageGroundEnemy;
			if (health.CurrentVal <= 0) {
				GameMaster.KillPlayer (this);
			}

		}

		if(col.gameObject.tag=="Coin"){
			Destroy(col.gameObject);
			coins++;
			coinText.text = "Coins : " + coins.ToString();
		}
	}

//	void OnTrigerEnter2D (Collider2D other){
//		Debug.Log ("oh");
//	
//	
//	}

	// Use this for initialization
	void Awake () {
		health.Initialize ();
	}



	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.T))
//			health.CurrentVal -= 10;
	}
}
