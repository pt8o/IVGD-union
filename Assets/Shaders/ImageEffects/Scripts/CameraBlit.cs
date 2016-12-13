using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof (Camera))]
public class CameraBlit : MonoBehaviour {

	public Material TransitionalMaterial;

	void OnRenderImage (RenderTexture src, RenderTexture dst) {

		Graphics.Blit (src, dst, TransitionalMaterial);

	}
	
}
