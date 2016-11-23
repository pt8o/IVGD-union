using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	private GameObject god;

	void Start () {
		god = GameObject.FindGameObjectWithTag ("God");
	}

	void OnTriggerEnter (Collider co) {
		if (co.gameObject.tag == "Player") {
			god.GetComponent<GodControl> ().platform.transform.FindChild ("Axis").gameObject.SetActive (false);
			god.GetComponent<GodControl> ().platform = this.gameObject;
			gameObject.transform.FindChild ("Axis").gameObject.SetActive (true);
		}
	}
}
