using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPS_Camera : MonoBehaviour 
{
	public static FPS_Camera instance;

	public float turnSpeed = 1.5f;
	public float turnSmoothing = .1f;
	public float tiltMin = -30f;
	public float tiltMax = 70f;

	private float lookAngle;
	private float tiltAngle;

	private float smoothX = 0f;
	private float smoothY = 0f;
	private float smoothVelocityX = 0f;
	private float smoothVelocityY = 0f;

	Transform pivot;
	Transform trans;

	public bool addShake;
	public float shakeAmountX = .1f;
	public float shakeAmountY = .2f;

	void Awake()
	{
		instance = this;

		pivot = transform.FindChild("Pivot_Cam");
		trans = GetComponent<Transform>();
	}

	void Start() 
	{
		if(!pivot)
			Debug.Log("There is no child Pivot_Cam detected. Check if it hasn't been renamed or missing.");
	}

	void FixedUpdate() 
	{
		HandleCameraMovement();
	}

	void HandleCameraMovement()
	{
		float x = FPS_PlayerInput.instance.mouseX;
		float y = FPS_PlayerInput.instance.mouseY;

		if(addShake) // Recoil
		{
			x += shakeAmountX;
			y += shakeAmountY;	
		}

		if(turnSmoothing > 0f)
		{
			smoothX = Mathf.SmoothDamp(smoothX, x, ref smoothVelocityX, turnSmoothing);
			smoothY = Mathf.SmoothDamp(smoothY, y, ref smoothVelocityY, turnSmoothing);
		}
		else
		{
			smoothX = x;
			smoothY = y;
		}

		lookAngle += smoothX * turnSpeed;
		trans.rotation = Quaternion.Euler(0f, lookAngle, 0f);

		tiltAngle -= smoothY * turnSpeed;
		tiltAngle = Mathf.Clamp(tiltAngle, tiltMin, tiltMax);

		pivot.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
	}
}