using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPS_PlayerInput : MonoBehaviour 
{
	public static FPS_PlayerInput instance;

	[Header("Axes")]
	public float mouseX;
	public float mouseY;
	[Range(-1f, 1f)]
	public float horizontal;
	[Range(-1f, 1f)]
	public float vertical;
	[Header("Buttons")]
	public bool fire1;
	public bool fire2;
	public bool fire3;

	void Awake()
	{
		instance = this;
	}

	void FixedUpdate()
	{
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		fire1 = Input.GetButton("Fire1");
		fire2 = Input.GetButton("Fire2");
		fire3 = Input.GetButton("Fire3");
	}
}
