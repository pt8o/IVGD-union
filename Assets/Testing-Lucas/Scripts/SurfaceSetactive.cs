using UnityEngine;
using System.Collections;

public class SurfaceSetactive : MonoBehaviour {

	private GameObject god;
	private GameObject parentPlatform;

	void Start () {
		god = GameObject.FindGameObjectWithTag ("God");
		parentPlatform = this.gameObject.transform.parent.transform.gameObject;
	}

	void OnCollisionEnter (Collision co) {
		if (co.gameObject.tag == "Player") {
			god.GetComponent<GodControl> ().platform.transform.FindChild ("Axis").gameObject.SetActive (false);
			god.GetComponent<GodControl> ().platform = parentPlatform;
			parentPlatform.transform.FindChild ("Axis").gameObject.SetActive (true);
		}
	}
}
