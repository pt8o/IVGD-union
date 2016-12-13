using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour {

	//This script will make the camera 
	//follow the target in a playful bouncy way
	//
	//To use it, put the camera as the child of the target
	//then position it so that it is at the right distance.
	//
	//This script will handle the rest.

	public Transform target;		//The entity the camera will follow
	public float followSpeed;		//The speed at which the camera will follow
	public float rotationSpeed;		//The speed at which the camera will rotate
	public bool shouldRotateAround;	//Whether or not the camera will circle the entity as it rotates

	private float radius;
	private Vector3 positionOffset;	//The initial distance from the target
	private Quaternion rotationOffset;	//The initial rotation of the camera

	private Vector3 finalPosition;
	private Quaternion finalRotation;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb =  GetComponent <Rigidbody> ();
		positionOffset = transform.position - target.position;
		rotationOffset = transform.rotation;
		transform.parent = null;

		radius = CalculateRadius (positionOffset.x, positionOffset.z);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (shouldRotateAround) {
			FindTargetPositionAround ();

			//transform.position = Vector3.MoveTowards (transform.position, finalPosition, rotationSpeed * Time.deltaTime);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, finalRotation, 360 * Time.deltaTime);

			rb.velocity = new Vector3 (rb.velocity.x * 0.5f, rb.velocity.y * 0.8f, rb.velocity.z * 0.5f);
			rb.AddForce ((new Vector3(finalPosition.x, 0.0f, finalPosition.z) - new Vector3 (transform.position.x, 0.0f, transform.position.z)) * rotationSpeed, ForceMode.Acceleration);
			rb.AddForce ((new Vector3(0.0f, finalPosition.y, 0.0f) - new Vector3 (0.0f, transform.position.y, 0.0f)) * followSpeed, ForceMode.Acceleration);


		} else {
			FindTargetPosition ();

			rb.AddForce ((finalPosition - transform.position) * followSpeed, ForceMode.Acceleration);
			rb.velocity *= 0.7f;
			//transform.position = Vector3.Lerp (transform.position, finalPosition, followSpeed * Time.deltaTime);

		}

	}

	float CalculateRadius (float xDist, float yDist) {
		return new Vector2 (xDist, yDist).magnitude;
	}

	void FindTargetPosition () {

		finalPosition = target.position + positionOffset;
	}

	void FindTargetPositionAround () {

		finalRotation = Quaternion.Euler (target.rotation.eulerAngles + rotationOffset.eulerAngles);

		float theta = (target.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
		finalPosition = new Vector3 (
			-radius * Mathf.Sin (theta) + target.position.x,
			target.position.y + positionOffset.y, 
			-radius * Mathf.Cos (theta) + target.position.z);
		
	}

}
