using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	public Renderer rend;
	public Animator anim;
	public Material normalMapFront;
	public Material normalMapBack;


	private bool isFacingLeft = true;
	private bool isFacingForward = true;
	private bool isWalking = false;

	// Use this for initialization
	void Start () {
		
		rend = GetComponent <Renderer> ();
		anim = GetComponent <Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		if (x != 0 || y != 0) {
			
			isWalking = true;
		
			if (x != 0)
				anim.SetFloat ("Horizontal", x);
			
			if (y != 0) {
				anim.SetFloat ("Vertical", y);

				if (y < 0) {
					//put front normal
					rend.material= normalMapFront;

				}

				if (y > 0) {
					//put back normal
					rend.material = normalMapBack;

				}

			}
				

		} else {
			isWalking = false;
		}

			
		anim.SetBool ("isWalking", isWalking);
		anim.SetBool ("isGrounded", GroundCheck ());

	}

	public bool GetDirectionBool (float direciton) {

		if (direciton > 0)
			return false;
		else if (direciton < 0)
			return true;

		return true;

	}

	public bool GroundCheck () {

		if (Physics.Raycast (transform.parent.position, -transform.parent.up, 2)) {
			return true;
		}

		return false;

	}

}
