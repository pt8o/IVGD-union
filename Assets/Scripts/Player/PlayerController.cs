using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public bool isVibrating = false;

	public float moveSpeed = 5.0f;			//Movement speed of the player in meters per second
	public float rotationSpeed = 250.0f;	//Rotation speed of the player in degress per second
	public float platformLaunchSpeed = 10.0f; // The minimum angular velocity that will send the player flying
	public float launchForce = 10.0f; 		// The force applied when launching

	public float slerpRate = 10.0f;

	private Transform guardian;				//A Reference to the guardian
	private Rigidbody rb;					//Reference to the player's Rigidbody
	private Rigidbody controllablePlatform; //Reference to the Rigidbody of the current Controllable Platform

	private float deathHeight = -100.0f;  	//y value cutoff for killing the player by falling
	private Transform spawnPoint;			//The point at which the player will spawn after dying
	private float timer = 0.0f;

	void Start () {
		rb = GetComponent <Rigidbody> ();

		if (!isLocalPlayer) {

			Destroy (GameObject.FindGameObjectWithTag ("World"));
			Destroy (GameObject.FindGameObjectWithTag ("Main Camera"));

			for (int i = 0; i < transform.childCount; i++) {

				if (transform.GetChild (i) != null) {
					Destroy (transform.GetChild (i));
				}
			}

		}
	}
	
	void FixedUpdate()  {
		
		if (!isLocalPlayer) {
			return;
		}

		//handle game logic here
		MovePlayer ();
		MovePlatform ();
		CheckIfDeadByFalling ();

		if (rb.velocity.y > 5.0f) {

			rb.velocity = new Vector3 (rb.velocity.x, 5.0f, rb.velocity.z);

		}

	}

	void Update()  {

		if (!isLocalPlayer) {

			if (isVibrating) {

				if (timer < 1.0f) {
					timer += Time.deltaTime;
				} else {
					isVibrating = false;
					timer = 0.0f;
				}

			}

			return;
		}

		if (isVibrating)
			print ("vibrating");

	}

	private void MovePlayer () {

		if (Input.GetKey (KeyCode.X)) {

			float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed;
			transform.Rotate (0.0f, x, 0.0f);
			UpdateRelativeAxes ();


		} else {

			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");
			rb.MovePosition(transform.position + (transform.forward * z + transform.right * x).normalized * Time.deltaTime * moveSpeed);

		}

	}

	//The last known rotation of the guardian
	Quaternion lastRotation = Quaternion.identity;
	Quaternion nextRotation = Quaternion.identity;

	private void MovePlatform () {

		if (controllablePlatform != null) {

			if (guardian == null) {

				//Get a reference to the guardian if there is none and update its rotation
				guardian = GameObject.FindGameObjectWithTag ("Guardian").transform;
				lastRotation = guardian.rotation;

			} else {

				//Find the change in rotation of the guardian
				Quaternion angleDelta = Quaternion.Inverse(lastRotation) * guardian.rotation;

				//Convert the x, y and z rotations to relative axes
				//Based on player rotation and device rotation
				Quaternion qX = Quaternion.AngleAxis(angleDelta.eulerAngles.x, relativeRight);
				Quaternion qY = Quaternion.AngleAxis(angleDelta.eulerAngles.y, relativeUp);
				Quaternion qZ = Quaternion.AngleAxis(angleDelta.eulerAngles.z, relativeForward);

				//Add transformed rotations together
				Quaternion rotAmount = qX * qY * qZ;

				//Rotate the platform
				controllablePlatform.MoveRotation (controllablePlatform.rotation * rotAmount);
				//nextRotation = controllablePlatform.rotation * rotAmount;

				//keep track of the last rotation
				lastRotation = guardian.rotation;
			
			}

		}

	}

	private void CheckIfDeadByFalling () {
		
		if (transform.position.y < deathHeight) {
			ResetPosition ();
			ResetPlatforms ();

			isVibrating = true;
		}

	}

	private void ResetPosition () {
		
		rb.velocity = Vector3.zero;

		if (spawnPoint == null) {
			spawnPoint = GameObject.FindGameObjectWithTag ("Spawn Point").transform;
		}
		
		transform.position = spawnPoint.position;

	}

	private void ResetPlatforms () {

		GameObject[] platforms = GameObject.FindGameObjectsWithTag ("Platform");

		for (int i = 0; i < platforms.Length; i++) {
			platforms[i].GetComponent <Rigidbody> ().MoveRotation (Quaternion.identity);
		}

		controllablePlatform = null;

	}


	void OnCollisionEnter (Collision other) {

		if (!isLocalPlayer) {
			return;
		}

		if (other.gameObject.CompareTag ("Platform")) {


			if (controllablePlatform == null) {
				
				controllablePlatform = other.gameObject.GetComponent<Rigidbody> ();
				UpdateRelativeAxes ();

			} else if (controllablePlatform != other.gameObject.GetComponent<Rigidbody> ()){
				
				controllablePlatform = other.gameObject.GetComponent<Rigidbody> ();
				UpdateRelativeAxes ();

			}

			rb.velocity = new Vector3 (0.0f, rb.velocity.y, 0.0f);

		}

	}

	void OnCollisionStay (Collision other) {

		if (!isLocalPlayer) {
			return;
		}

	}



	void OnCollisionExit (Collision other) {

		if (!isLocalPlayer) {
			return;
		}

	}

	Vector3 relativeRight = Vector3.right;
	Vector3 relativeUp = Vector3.up;
	Vector3 relativeForward = Vector3.forward;

	void UpdateRelativeAxes () {

		relativeRight = guardian.TransformDirection(Vector3.right);
		relativeUp = guardian.TransformDirection(Vector3.up);
		relativeForward = guardian.TransformDirection(Vector3.forward);

		relativeRight = transform.InverseTransformDirection (relativeRight);
		relativeUp = transform.InverseTransformDirection (relativeUp);
		relativeForward = transform.InverseTransformDirection (relativeForward);

		if (controllablePlatform != null) {

			relativeRight = controllablePlatform.transform.InverseTransformDirection (relativeRight);
			relativeUp = controllablePlatform.transform.InverseTransformDirection (relativeUp);
			relativeForward = controllablePlatform.transform.InverseTransformDirection (relativeForward);

		}


	}


		
}