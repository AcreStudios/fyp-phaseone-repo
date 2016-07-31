using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPS_Movement : MonoBehaviour 
{
	public float runSpeed = 60f;
	public float walkSpeed = 35;
	public float aimSpeed = 15;
	[SerializeField]
	private float moveSpeed;

	Rigidbody rigidB;
	Transform playerParent;

	public AudioSource[] footsteps;
	private float footstepTimer = .5f;
	private float footstepCounter = 0f;

	void Awake()
	{
		rigidB = GetComponent<Rigidbody>();
		playerParent = transform.FindChild("Parent_Player");
	}

	void Start() 
	{
		if(!playerParent)
			Debug.Log("There is no child Parent_Player detected. Check if it hasn't been renamed or missing.");
	}

	void FixedUpdate() 
	{
		HandlePlayerMovement();
	}

	void HandlePlayerMovement()
	{
		moveSpeed = (FPS_PlayerInput.instance.fire2) ? aimSpeed : walkSpeed;

		if(!FPS_PlayerInput.instance.fire2 && FPS_PlayerInput.instance.fire3) // Running, fire3 is temp placeholder
			moveSpeed = runSpeed;

		rigidB.AddForce(playerParent.forward * FPS_PlayerInput.instance.vertical * moveSpeed);
		rigidB.AddForce(playerParent.right * FPS_PlayerInput.instance.horizontal * moveSpeed);

		if(FPS_PlayerInput.instance.vertical != 0f || FPS_PlayerInput.instance.horizontal != 0f)
		{
			footstepCounter += Time.deltaTime;
			if(footstepCounter > footstepTimer)
			{
				footstepCounter = 0f;
				PlayFootsteps();
			}
		}
			
	}

	void PlayFootsteps()
	{
		int chooseIndex = Random.Range(0, 4);

		footsteps[chooseIndex].Play();
	}
}