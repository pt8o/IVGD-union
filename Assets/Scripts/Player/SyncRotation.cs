using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncRotation : NetworkBehaviour {

	[SyncVar]
	private Quaternion syncRot = new Quaternion(0,0,0,1);

	[SerializeField]Transform rb;
	[SerializeField]float slerpRate = 15;

	void FixedUpdate () {

		TransmitRotation ();

	}

	void Update () {

		SlerpRotation ();

	}

	void SlerpRotation () {
		if (!isLocalPlayer) {
			transform.rotation = Quaternion.Slerp (rb.rotation, syncRot, Time.deltaTime * slerpRate);
		}
	}

	[Command]
	void CmdProvideRotationToServer (Quaternion rot) {
		syncRot = rot;
	}

	[ClientCallback]
	void TransmitRotation () {

		if (isLocalPlayer) {
			CmdProvideRotationToServer (rb.rotation);
		}

	}

}
