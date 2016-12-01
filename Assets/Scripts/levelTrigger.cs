using UnityEngine;
using System.Collections;

public class levelTrigger : MonoBehaviour {

	private bool isHit = false;

	void OnTriggerEnter(Collider co) {
		
		if (isHit == false && co.gameObject.tag == "Player") {
			GameObject.Find ("LevelMaster").GetComponent<levelLoad> ().levelTrigger ();
			Destroy (this.gameObject);
		}
		isHit = true;
	}
}
