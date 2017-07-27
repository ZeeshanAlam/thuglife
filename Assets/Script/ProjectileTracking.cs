using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ProjectileTracking : MonoBehaviour {

	public float maxStretch = 3.0f;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;

	private SpringJoint2D spring;
	private Ray rayToMouse;
	private Transform catapult;
	private Ray LeftCatapultToProjectile;
	private float maxStretchSqr;
	private float circleRadius;
	private bool clickedOn;
	private Vector2 prevVelocity;
	public GameObject asteroid;
	public GameObject gulelback;
	public GameObject gulelfront;
	private GameObject currentAsteroid;
	public float time=1f;
	public bool flag= false;

	[Tooltip("Area where the draw must start from. The bow will not start drawing an arrow unless the click happens in this area.")]
	public Collider2D DrawStartArea;

//	[Tooltip("Area where the draw must stay in. If the click moves outside this area, the draw fizzles.")]
//	public Collider2D DrawTotalArea;


	void Awake(){
		catapult = gulelback.transform;

	}

	void Start () {
		LineRendererSetup ();
		catapultLineFront = gulelfront.transform.GetComponent<LineRenderer>();
		catapultLineBack = gulelback.transform.GetComponent<LineRenderer>();
		rayToMouse = new Ray (catapult.position, Vector3.zero);
		asteroid.GetComponent<Rigidbody2D> ().isKinematic = true;
		LeftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = maxStretch * maxStretch;
		CircleCollider2D circle = asteroid.GetComponent<Collider2D>() as CircleCollider2D;
		circleRadius = circle.radius;
		//catapultLineBack = gulelback.transform.parent.GetComponent<LineRenderer>();


	}
	//public event System.EventHandler BirdThrown;
	void Update () {

		LineRendererSetup ();
		catapultLineFront = gulelfront.transform.GetComponent<LineRenderer>();
		catapultLineBack = gulelback.transform.GetComponent<LineRenderer>();
		rayToMouse = new Ray (catapult.position, Vector3.zero);
		LeftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);
		if (clickedOn)
			Dragging ();

		if(Input.GetMouseButtonDown(0)){
				prevVelocity = Vector2.zero;
				catapultLineFront.enabled = true;
				catapultLineBack.enabled = true;

			Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (DrawStartArea.OverlapPoint(worldPos))
			{
				BeginBowDraw ();
				spring = currentAsteroid.GetComponent<SpringJoint2D>();
				spring.enabled = false;
				currentAsteroid.GetComponent<Rigidbody2D>().isKinematic = true;;
				clickedOn = true;
				flag = true;
			}

			delay ();
			}
		if (Input.GetMouseButtonUp(0)&&flag==true)
			 {
				spring.enabled = true;
			    currentAsteroid.GetComponent<Rigidbody2D>().isKinematic= false;
				clickedOn = false;
				flag = false;
			}

		if(spring!=null){

			if(!currentAsteroid.GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > currentAsteroid.GetComponent<Rigidbody2D>().velocity.sqrMagnitude){
				Destroy (spring);
				currentAsteroid.GetComponent<Rigidbody2D>().velocity = prevVelocity;
			}
			if (!clickedOn){

				prevVelocity = currentAsteroid.GetComponent<Rigidbody2D>().velocity;
				delay ();
			} 
			LineRendererUpdate ();

		} else {
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
			Destroy (currentAsteroid, 5);
		}
	}

	IEnumerator delay(){
		yield return new WaitForSeconds (time);
	}

	private void BeginBowDraw()
	{
		// start drawing back the bow

		// play a knock arrow sound
		//PlayRandomSound(KnockClips);

		// create a new arrow that will be fired later
		currentAsteroid = GameObject.Instantiate(asteroid);
		currentAsteroid.transform.position = gulelfront.transform.position;

		currentAsteroid.SetActive(true);
		//asteroid = currentAsteroid;
		//spring = asteroid.GetComponent<SpringJoint2D>();
		//spring.enabled = false;
		// find where the arrow should be based on where they started drawing back the bow
		//Vector3 pos = GetArrowPositionForDraw();
		//currentArrow.transform.position = pos;
		//drawingBow = true;
	}


	void LineRendererSetup() {
		catapultLineFront.SetPosition (0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition (0, catapultLineBack.transform.position);

		//Debug.Log (catapultLineFront.transform.position);

		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;

	}


	void OnMouseDown() {
		spring.enabled = false;
		currentAsteroid.GetComponent<Rigidbody2D>().isKinematic = true;;
		clickedOn = true;
	}

	void OnMouseUp() {
		spring.enabled = true;
		currentAsteroid.GetComponent<Rigidbody2D>().isKinematic= false;
		clickedOn = false;
	}


	void Dragging(){
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStretchSqr){
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);
		}
		//Debug.Log (mouseWorldPoint.magnitude);

		mouseWorldPoint.z = 0f;
		currentAsteroid.transform.position = mouseWorldPoint;
	}


	void LineRendererUpdate(){
		
		Vector2 catapultToProjectile = currentAsteroid.transform.position - catapultLineFront.transform.position;
		LeftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = LeftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude + circleRadius);
		//holdPoint=Vector3.zero;
		//Debug.Log(holdPoint);
		catapultLineFront.SetPosition (1, holdPoint);
		catapultLineBack.SetPosition (1, holdPoint);
	}

}
