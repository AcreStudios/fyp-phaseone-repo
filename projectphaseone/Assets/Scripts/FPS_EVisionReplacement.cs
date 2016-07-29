using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FPS_EVisionReplacement : MonoBehaviour
{
	public Shader XRayShader;

	void OnEnable()
	{
		GetComponent<Camera>().SetReplacementShader(XRayShader, "EVision");
	}
}