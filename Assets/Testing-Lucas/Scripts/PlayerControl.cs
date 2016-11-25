using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed;
	public float jumpForce;

	private KeyCode keyJump;
	private bool grounded = true;

	private Rigidbody rigidB;

	void Start () {
		keyJump = KeyCode.Space;
		rigidB = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidB.MovePosition (transform.position + movement * speed * Time.deltaTime);

		if (Input.GetKeyDown(keyJump) && grounded){
			rigidB.AddForce (new Vector3 (0.0f, jumpForce, 0.0f) * speed); 
		}	
	}
}