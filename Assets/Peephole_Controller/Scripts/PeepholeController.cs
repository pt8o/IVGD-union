using UnityEngine;
using System.Collections;

public class PeepholeController : MonoBehaviour {

	public Transform referenceRot;

	public Quaternion lastRotation;
	public Quaternion currentRotation;

	void Start () {
		currentRotation = DeviceRotation.Get ();
		lastRotation = currentRotation;
	}
	
	// Update is called once per frame
	void Update () {

		//transform.Rotate (0, 0, -Input.gyro.rotationRateUnbiased.y * 2.0f);
	
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

	public float GetSignedAngleDelta (float currentAngle, float lastAngle) {

		float deltaA = currentAngle - lastAngle;
		float deltaB = lastAngle - currentAngle;

		if (Mathf.Abs (deltaA) < 180)
			return deltaA;
		else 
			return deltaB;

	}

	public float mod (float a, float n) {

		float modulo = a - Mathf.Floor(a/n) * n;


		return modulo;

	}
}
