using UnityEngine;
using System.Collections;

public class CameraVision : MonoBehaviour {

	//This script will check if there are any platforms 
	//between the player and the camera

	public Transform cam;
	public Transform player;
	public LayerMask solidMask;

	void Update () {

		RayCastToPlayerFrom (cam.position);
	
	}

	private void RayCastToPlayerFrom (Vector3 startPoint) {

		Vector3 directionToPlayer = player.position - startPoint;  //Get the vector from the camera to the player

		RaycastHit[] hits;
		hits = Physics.RaycastAll (startPoint, directionToPlayer, directionToPlayer.magnitude, solidMask);

		for (int i = 0; i < hits.Length; i++) {
			HideViewObstuction (hits[i].collider.gameObject);
		}
			
	}

	private void HideViewObstuction (GameObject go) {

		ShowHideObject other = go.GetComponent <ShowHideObject> ();

		if (other == null) {
			other = go.AddComponent <ShowHideObject> ();
		}

		other.Hide ();

	}

}
