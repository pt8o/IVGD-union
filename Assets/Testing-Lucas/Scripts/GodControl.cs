using UnityEngine;
using System.Collections;

public class GodControl : MonoBehaviour {

	public float horizontalSpeed = 5.0F;
	public GameObject platform;

	void Update() {
		float h = horizontalSpeed * Input.GetAxis("Mouse X");
		platform.transform.Rotate(0, 0, -h);
	}
}
