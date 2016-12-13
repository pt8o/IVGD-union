using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof (Camera))]
public class CameraRenderDepth : MonoBehaviour {
	
	public Material mat;

	void OnEnable () {
		GetComponent <Camera> ().depthTextureMode = DepthTextureMode.Depth;
	}

	void Update (){
		if (Input.GetKeyDown(KeyCode.E)){
			//set _StartingTime to current time
			mat.SetFloat("_StartingTime", Time.time);
			//set _RunRingPass to 1 to start the ring
			mat.SetFloat("_RunRingPass", 1);
		}
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination){
		Graphics.Blit(source,destination,mat);
	}

}
