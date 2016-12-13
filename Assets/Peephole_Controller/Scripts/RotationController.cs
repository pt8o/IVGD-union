using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour {

	public float speed = 1f;
	
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR

		HandleInputs(Input.GetAxisRaw ("Horizontal"));
		return;

		#endif

		Quaternion referenceRotation = Quaternion.identity;
		Quaternion deviceRotation = DeviceRotation.Get();
		Quaternion eliminationOfXZ = Quaternion.Inverse(
			Quaternion.FromToRotation(referenceRotation * Vector3.up, 
				deviceRotation * Vector3.up)
		);
		Quaternion rotationY = eliminationOfXZ * deviceRotation;
		float roll = rotationY.eulerAngles.y;

		roll -= 56f;
		roll = - roll;

		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, roll));
	
	}

	void HandleInputs (float axis) {

		transform.Rotate (0, 0, -axis * Time.deltaTime * speed);

	}

	public static class DeviceRotation {
		private static bool gyroInitialized = false;

		public static bool HasGyroscope {
			get {
				return SystemInfo.supportsGyroscope;
			}
		}

		public static Quaternion Get() {
			if (!gyroInitialized) {
				InitGyro();
			}

			return HasGyroscope
				? ReadGyroscopeRotation()
					: Quaternion.identity;
		}

		private static void InitGyro() {
			if (HasGyroscope) {
				Input.gyro.enabled = true;                // enable the gyroscope
				Input.gyro.updateInterval = 0.0167f;    // set the update interval to it's highest value (60 Hz)
			}
			gyroInitialized = true;
		}

		private static Quaternion ReadGyroscopeRotation() {
			return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
		}
	}

}
