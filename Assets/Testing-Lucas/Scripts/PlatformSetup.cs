using UnityEngine;
using System.Collections;

public class PlatformSetup : MonoBehaviour {


	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if (child.CompareTag ("Surface")) {
				child.transform.gameObject.AddComponent<SurfaceSetactive> ();
			}	
		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
