using UnityEngine;
using System.Collections;

public class EnemyStat : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp (value, 0, maxHealth); }
		}


		public int damage = 10;

		public void Init()
		{
			curHealth = maxHealth;
		}
	}

	public EnemyStats stats = new EnemyStats();

	public Transform deathParticles;
	public string bee = "Bee";
	AudioManager audioManager;

	public float shakeAmt = 0.1f;
	public float shakeLength = 0.1f;

	[Header("Optional: ")]
	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start()
	{
		stats.Init();

		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("No audiomanager found!");
		}

		if (statusIndicator != null)
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}

		if (deathParticles == null)
		{
			Debug.LogError("No death particles referenced on Enemy");
		}
		audioManager.PlaySound (bee);
	}

	public void DamageEnemy (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			GameMaster.KillEnemy (this);
		}

		if (statusIndicator != null)
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		//PlayerManager _player = _colInfo.collider.GetComponent<Player>();
		if (_colInfo.gameObject.tag == "Asteroid"||_colInfo.gameObject.tag == "Arrow")
		{
			Debug.Log ("Hit it");
			//_player.DamagePlayer(stats.damage);
			DamageEnemy(10);
			Destroy (_colInfo.gameObject, 0.05f);
		}
	}
}
