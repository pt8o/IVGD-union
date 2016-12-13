using UnityEngine;
using UnityEngine.Networking;

public class GuardianController : NetworkBehaviour {

	public static Quaternion rota;

	Quaternion lastRotation = Quaternion.identity;
	Rigidbody rb;

	private bool isInitialized = false;

	public override void OnStartLocalPlayer() {
		lastRotation = DeviceRotation.Get ();
		rb = GetComponent <Rigidbody> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update() {
		
		if (!isLocalPlayer)
			return;

		if (!isInitialized) {

			if (Input.deviceOrientation == DeviceOrientation.FaceUp) {
				isInitialized = true;
			}

		} else {

			Quaternion angleDelta = Quaternion.Inverse(lastRotation) * DeviceRotation.Get ();
			Quaternion rot = Quaternion.Euler (-angleDelta.eulerAngles.x, -angleDelta.eulerAngles.z, -angleDelta.eulerAngles.y);
			rb.MoveRotation (rb.rotation * rot);

			lastRotation = DeviceRotation.Get ();

		}

	}


	Vector3 GetCorrectAngle (Vector3 eulerAngles) {

		float x = 0;
		if (eulerAngles.x > 180)
			x = eulerAngles.x - 360;
		else 
			x = eulerAngles.x;

		float y = 0;
		if (eulerAngles.y > 180)
			y = eulerAngles.y - 360;
		else 
			y = eulerAngles.y;


		float z = 0;
		if (eulerAngles.z > 180)
			z = eulerAngles.z - 360;
		else 
			z = eulerAngles.z;

		return new Vector3 (x, y, z);

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
			return Input.gyro.attitude;
		}
	}
		
}
