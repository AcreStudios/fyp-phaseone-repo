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
	private Camera cam;
	private Camera EVcam;
	private FPS_EVisionReplacement EVreplace;

	[Range(0f,1f)]
	public float progress = 0f;

	private bool switchedToEV = false;

	void Awake()
	{
		vignette = GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
		noise = GetComponent<UnityStandardAssets.ImageEffects.NoiseAndGrain>();
		reflection = GetComponent<UnityStandardAssets.CinematicEffects.ScreenSpaceReflection>();

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
		vignette.intensity = Mathf.Lerp(0f, .33f, progress);
		vignette.blur = Mathf.Lerp(0f, .66f, progress);
		noise.intensityMultiplier = Mathf.Lerp(0f, 4f, progress);

		if(FPS_PlayerInput.instance.eBtn)
		{
			if(!switchedToEV)
			{
				StopAllCoroutines();
				StartCoroutine(SwitchToVision());
			}
			else
			{
				StopAllCoroutines();
				StartCoroutine(SwitchToNormal());
			}
		}
	}

	IEnumerator SwitchToVision()
	{
		cam.renderingPath = RenderingPath.Forward;
		EVreplace.enabled = true;
		reflection.enabled = false;

		while(progress < 1f)
		{
			progress += Time.deltaTime;
			yield return null;
		}

		switchedToEV = true;
	}

	IEnumerator SwitchToNormal()
	{
		cam.renderingPath = RenderingPath.DeferredShading;
		EVreplace.enabled = false;
		EVcam.ResetReplacementShader();
		reflection.enabled = true;

		while(progress > 0f)
		{
			progress -= Time.deltaTime;
			yield return null;
		}

		switchedToEV = false;
	}
}
