using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
	public CameraShake cameraShake;
	public static GameMaster gm;

	void Awake()
	{
		if (gm == null)
		{
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	void Start()
	{
		if (cameraShake == null)
		{
			Debug.LogError("No camera shake referenced in GameMaster");
		}
	}


	public static void KillPlayer(PlayerManager player){
		Destroy (player.gameObject);
	}

	public static void KillEnemy(EnemyStat enemy)
	{
		gm._KillEnemy(enemy);
	}

	public void _KillEnemy(EnemyStat _enemy)
	{
		// Let's play some sound
		//audioManager.PlaySound(_enemy.deathSoundName);

		// Gain some money
//		Money += _enemy.moneyDrop;
//		audioManager.PlaySound("Money");

		// Add particles
		Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
		Destroy(_clone.gameObject, 5f);



		// Go camerashake
		cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		Destroy(_enemy.gameObject);
	}

	public static void KillGroundEnemy(GroundEnemy enemy)
	{
		gm._KillGroundEnemy(enemy);
	}

	public void _KillGroundEnemy(GroundEnemy _enemy)
	{
		// Let's play some sound
		//audioManager.PlaySound(_enemy.deathSoundName);

		// Gain some money
		//		Money += _enemy.moneyDrop;
		//		audioManager.PlaySound("Money");

		// Add particles
		Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;


		    Destroy(_clone.gameObject, 5f);


		// Go camerashake
		cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		if (_enemy.transform.parent != null)
			Destroy (_enemy.transform.parent.gameObject);
		else
		Destroy(_enemy.gameObject);
	}

}
