using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
[RequireComponent(typeof(UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration))]
[RequireComponent(typeof(UnityStandardAssets.ImageEffects.NoiseAndGrain))]
public class FPS_EVision : MonoBehaviour 
{
	public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration vignette;
	public UnityStandardAssets.ImageEffects.NoiseAndGrain noise;
	public UnityStandardAssets.CinematicEffects.ScreenSpaceReflection reflection;
	public UnityStandardAssets.ImageEffects.BloomOptimized bloomO;

	private Camera cam;
	private Camera EVcam;
	private FPS_EVisionReplacement EVreplace;

	[Range(0f,1f)]
	public float progress = 0f;

	private bool switchedToEV = false;
	private bool isSwitching = false;

	[Header("Effects")]
	public AudioSource eVisionOnSFX;
	public AudioSource eVisionOffSFX;

	void Awake()
	{
		vignette = GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
		noise = GetComponent<UnityStandardAssets.ImageEffects.NoiseAndGrain>();
		reflection = GetComponent<UnityStandardAssets.CinematicEffects.ScreenSpaceReflection>();
		//bloomO = EVcam.transform.GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>();

		cam = GetComponent<Camera>();
		EVcam = cam.transform.GetChild(0).GetComponent<Camera>();

		EVreplace = EVcam.GetComponent<FPS_EVisionReplacement>();
	}

	void Start() 
	{	
		// EVision progress
		progress = 0f;

		// Initialise camera;
		cam.renderingPath = RenderingPath.DeferredShading;

		// Initialise the image effects
		vignette.intensity = 0;
		vignette.blur = 0;
		vignette.blurDistance = 0.5f;

		noise.intensityMultiplier = 0;
		noise.blackIntensity = 0f;
		noise.whiteIntensity = 0f;
		noise.midGrey = 0f;
		noise.generalIntensity = 0.33f;
		noise.intensities = new Vector3(0f, 1f, 1f);
	}

	void Update() 
	{
		EVcam.fieldOfView = Camera.main.fieldOfView;

		vignette.intensity = Mathf.Lerp(0f, .33f, progress);
		vignette.blur = Mathf.Lerp(0f, .66f, progress);
		noise.intensityMultiplier = Mathf.Lerp(0f, 5f, progress);
		bloomO.intensity = Mathf.Lerp(0f, 1f, progress);

		if(FPS_PlayerInput.instance.eBtn)
		{
			if(!switchedToEV && !isSwitching)
				StartCoroutine(SwitchToVision());
			else if(switchedToEV && !isSwitching)
				StartCoroutine(SwitchToNormal());
		}

		Shader.SetGlobalFloat("_GlobalEVision", progress);
	}

	IEnumerator SwitchToVision()
	{
		cam.renderingPath = RenderingPath.Forward;
		EVcam.enabled = true;
		EVreplace.enabled = true;
		reflection.enabled = false;

		eVisionOnSFX.Play();
		isSwitching = true;

		while(progress < 1f)
		{
			progress += Time.deltaTime;
			yield return null;
		}

		switchedToEV = true;
		isSwitching = false;
	}

	IEnumerator SwitchToNormal()
	{
		cam.renderingPath = RenderingPath.DeferredShading;
		//EVcam.enabled = false;
		//EVreplace.enabled = false;
		//EVcam.ResetReplacementShader();
		reflection.enabled = true;

		eVisionOffSFX.Play();
		isSwitching = true;

		while(progress > 0f)
		{
			progress -= Time.deltaTime * 2f;
			yield return null;
		}

		switchedToEV = false;
		isSwitching = false;

		//cam.renderingPath = RenderingPath.DeferredShading;
		EVcam.enabled = false;
		EVreplace.enabled = false;
		EVcam.ResetReplacementShader();
		//reflection.enabled = true;
	}
}
