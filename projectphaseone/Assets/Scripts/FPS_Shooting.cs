using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPS_Shooting : MonoBehaviour 
{
	[Header("Shooting")]
	public float fireDamage = 20f;
	public float fireRate = 0.1f;
	private WaitForSeconds fRate;
	private bool canFire;

	public float shootRange = 100f;

	[Header("Recoil")]
	public float changeShakeSpeed = 50f;
	private float targetShake;

	[Header("Iron Sight")]
	public float ironSightFOV = 40f;
	public float normalFOV = 60f;
	private float targetFOV;

	[SerializeField]
	private Vector3 cameraOriginalPosition;
	private Transform cameraShakeTrans;
	private Camera cam;
	
	private Vector3 ironSightPosition;
	private Quaternion ironSightRotation;
	private Vector3 normalPosition;
	private Quaternion normalRotation;
	private Transform weaponTrans;
	private Transform ironSightTrans;
	private Vector3 targetPos;
	private Quaternion targetRot;
	public float changeFOVSpeed = 5f;
	public float switchSpeed = 20f;

	[Header("Effects")]
	//public ParticleSystem[] muzzleFlash;
	public AudioSource gunShotSFX;
	public GameObject impactParticlePrefab;
	//public Animator weaponAnimator;

	public bool debugAim = false;

	void Awake()
	{
		cameraShakeTrans = transform.Find("Parent_Player/Pivot_Cam/Parent_Cam");
		weaponTrans = transform.Find("Parent_Player/Pivot_Cam/Parent_Weapon");
		ironSightTrans = transform.Find("Parent_Player/Pivot_Cam/Trans_IronSight");
		cam = Camera.main;
	}

	void Start() 
	{
		if(!cameraShakeTrans)
			Debug.Log("There is no child Parent_Cam detected. Check if it hasn't been renamed or missing.");
		if(!weaponTrans)
			Debug.Log("There is no child Parent_Weapon detected. Check if it hasn't been renamed or missing.");
		if(!ironSightTrans)
			Debug.Log("There is no child Trans_IronSight detected. Check if it hasn't been renamed or missing.");

		// FOV
		cam.fieldOfView = normalFOV;

		// Positions
		cameraOriginalPosition = cameraShakeTrans.localPosition;
		ironSightPosition = ironSightTrans.localPosition;
		ironSightRotation = ironSightTrans.localRotation;
		normalPosition = weaponTrans.localPosition;
		normalRotation = weaponTrans.localRotation;

		// For shooting
		fRate = new WaitForSeconds(fireRate);
		canFire = true;

		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate() 
	{
		#region Iron Sight
		if(FPS_PlayerInput.instance.fire2 || debugAim)
		{
			// Set targets
			targetPos = ironSightPosition;
			targetRot = ironSightRotation;
			targetFOV = ironSightFOV;
			targetShake = 0f;

			cameraShakeTrans.localPosition = Vector3.Lerp(cameraShakeTrans.localPosition, cameraOriginalPosition, 1f);
		}
		else
		{
			// Set targets
			targetPos = normalPosition;
			targetRot = normalRotation;
			targetFOV = normalFOV;
			if(!debugAim)
				targetShake = .1f;
		}
		#endregion
		#region Shooting
		if(FPS_PlayerInput.instance.fire1 && canFire)
		{
			StartCoroutine(OverrideCameraInput());

			canFire = false;
			StartCoroutine(EnableFiring());

			RaycastBullet();
			//EmitMuzzleFlash();
			//PlayGunShotSound();

			//weaponAnimator.SetBool("Fire", true);
		}
		else
		{
			if(!FPS_PlayerInput.instance.fire2 && !debugAim)
				targetShake = .1f;

			//weaponAnimator.SetBool("Fire", false);
		}
		#endregion
		#region Moving
		if(FPS_PlayerInput.instance.horizontal != 0 || FPS_PlayerInput.instance.vertical != 0)
		{
			if(!FPS_PlayerInput.instance.fire2)
				targetShake = .5f;
			//weaponAnimator.SetFloat("Movement", (!FPS_PlayerInput.instance.fire3) ? .5f : 1f, .1f, Time.deltaTime);
		}
		else
		{
			if(!FPS_PlayerInput.instance.fire2 && !debugAim)
				targetShake = .1f;
			
			//weaponAnimator.SetFloat("Movement", 0f, .1f, Time.deltaTime);
		}
		#endregion
		#region Meeting the targets
		// Switching weapon transforms
		weaponTrans.localPosition = Vector3.Lerp(weaponTrans.localPosition, targetPos, switchSpeed * Time.deltaTime);
		weaponTrans.localRotation = Quaternion.Slerp(weaponTrans.localRotation, targetRot, switchSpeed * Time.deltaTime);

		// Camera shake
		FPS_CameraShake.instance.shakeSpeed = Mathf.MoveTowards(FPS_CameraShake.instance.shakeSpeed, targetShake, changeShakeSpeed * Time.deltaTime);

		// Changing FOV
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, changeFOVSpeed * Time.deltaTime );
		#endregion
	}

	IEnumerator EnableFiring()
	{
		yield return fRate;

		canFire = true;
	}

	IEnumerator OverrideCameraInput()
	{
		FPS_Camera.instance.addShake = true;

		yield return new WaitForSeconds(.1f);

		FPS_Camera.instance.addShake = false;
	}

	void RaycastBullet()
	{
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);
		RaycastHit hit;

		// Play gun shot sound
		gunShotSFX.Play();

		Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 1f);
		if(Physics.Raycast(ray, out hit, shootRange, LayerMask.NameToLayer("Shootable")))
		{
			// Spawn impact prefab
			GameObject impactParticle = (GameObject)Instantiate(impactParticlePrefab, hit.point, Quaternion.identity);
			impactParticle.transform.LookAt(transform.position);
			Destroy(impactParticle, 1f);

			// Damage object/enemies
			if(hit.transform.GetComponent<AIBase>())
				hit.transform.GetComponent<AIBase>().DamageRecieved(fireDamage);

			Debug.Log(hit.transform.name);
		}
	}
}