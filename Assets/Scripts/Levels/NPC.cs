using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public GameObject speechBubble;
	

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.CompareTag ("Player")) {

			speechBubble.SetActive (true);

		}

	}

	void OnTriggerExit (Collider other) {

		if (other.gameObject.CompareTag ("Player")) {

			speechBubble.SetActive (false);

		}

	}

}
