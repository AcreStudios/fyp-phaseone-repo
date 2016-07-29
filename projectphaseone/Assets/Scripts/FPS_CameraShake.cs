using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPS_CameraShake : MonoBehaviour 
{
	public static FPS_CameraShake instance;

	public float shakeSpeed = .1f;
	public Vector3 shakeRange = new Vector3(.2f, -.1f, 0f);

	private Vector3 pos;

	private Transform trans;

	void Awake()
	{
		instance = this;

		trans = GetComponent<Transform>();
	}

	void Start() 
	{
		pos = transform.localPosition;
	}

	void Update() 
	{
		if(shakeSpeed > 0f)
			trans.localPosition = pos + Vector3.Scale(SmoothRandom.GetVector3(shakeSpeed), shakeRange);
	}
}
