using UnityEngine;
using System.Collections;

public class PeepholeMovement : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	// Use this for initialization
	void Awake () {
		rb = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {

			rb.MovePosition (transform.position + (transform.forward * speed * Time.deltaTime));

		}
	
	}

}
