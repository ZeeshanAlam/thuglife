using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]

public class Enemy : MonoBehaviour {

	public Transform target;

	//How many times each second we will update our path
	public float updateRate =2f;

	//caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//the calculated path
	public Path path;

	//The AI's speed/sec
	public float speed =300f;
	public ForceMode2D fMode;

	public bool pathIsEnded = false;

	//the max distance from the AI to a waypoint for it to continue to the next point 
	public float nextwWaypointDistance = 3;

	//The waypoint we currently moving towards
	private int currentWaypoint = 0;
	private bool searchingForPlayer=false;

	void Start(){
	
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
					
			return;
		}
		// start a new path to the target position, return the result to the OnPathComplete fmethod
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	IEnumerator SearchForPlayer(){
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if(sResult ==null){
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		}
			else{
			searchingForPlayer = false;
			target = sResult.transform;
			StartCoroutine (UpdatePath ());
			yield return false;
		}
	}

	IEnumerator UpdatePath(){
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			yield return false;
		}
	
		seeker.StartPath(transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds (1f / updateRate);

		StartCoroutine (UpdatePath ());
	}


	public void OnPathComplete (Path p ){
		//Debug.Log ("We got it. If not " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		
		}
	}

	void FixedUpdate(){
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			return;
		}
		if (path == null)
			return;
		
		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			//Debug.Log("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//Direction to the next waypoint

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		float dist =Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
		//Move the enemy
		rb.AddForce(dir, fMode);
		if(dist<nextwWaypointDistance){
			currentWaypoint++;
			return;
		}
	}
}
