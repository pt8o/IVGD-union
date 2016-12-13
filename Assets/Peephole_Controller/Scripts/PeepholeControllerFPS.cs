using UnityEngine;
using System.Collections;

public class PeepholeControllerFPS : MonoBehaviour {

	public float speed = 1f;
	public Camera cam;
	
	// Update is called once per frame
	void Update () {

		Quaternion deviceRotation = DeviceRotation.Get ();

		transform.rotation = Quaternion.Euler (0.0f, deviceRotation.eulerAngles.y, 0.0f);
		cam.transform.rotation = Quaternion.Euler (deviceRotation.eulerAngles.x, deviceRotation.eulerAngles.y, 0.0f);
	
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
