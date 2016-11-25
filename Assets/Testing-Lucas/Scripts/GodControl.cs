using UnityEngine;
using System.Collections;

public class GodControl : MonoBehaviour {

	public float rotateSpeed = 5.0F;
	public GameObject platform;

	void Update() {
		float h = rotateSpeed * Input.GetAxis("Mouse X");
		float v = rotateSpeed * Input.GetAxis("Mouse Y");
		float g = rotateSpeed * Input.GetAxis ("godHorizontal");
		platform.transform.Rotate(-v, -h, g);
	}
}
