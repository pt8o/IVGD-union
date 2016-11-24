using UnityEngine;
using System.Collections;


//quick little script for the lighting example.
public class mouseFollow : MonoBehaviour {

   

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);


    }
}
