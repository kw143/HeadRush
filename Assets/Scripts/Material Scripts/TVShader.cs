using UnityEngine;
using System.Collections;


public class TVShader : MonoBehaviour
{
	public Material _material;
	[Header("Noise")]
	public bool doNoise = true;
	[Header("Vertical Shift")]
	public bool doVerticalShift = true;
	[Header("Color Shift ")]
	public bool doColorShift = true;
	public int colorShiftRand = 700;
	public float colorShiftLerp = 0.03f;
	public float colorShiftMin = 0.003f;
	public float colorShiftMax = 0.05f;
	[Header("Distortion")]
	public bool doDistortion = true;
	public int distortionRand = 15;
	public float disortionMin = 100;
	public float disortionMax = 200;
	[Header("Tan Distortion")]
	public bool doTanDistortion = true;

	float tanDir = 0;

	void Awake()
	{
		//_material = new Material(Shader.Find("Custom/VHSeffect"));
		//_material.SetTexture("_SecondaryTex", Resources.Load("Textures/TVnoise") as Texture);
		_material.SetFloat("_OffsetPosY", 0f);
		_material.SetFloat("_OffsetColor", 0.05f);
		_material.SetFloat("_OffsetDistortion", 480f);
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		// TV noise
		if (doNoise)
		{
			_material.SetFloat("_OffsetNoiseX", Random.Range(0f, 0.6f));
			float offsetNoise = _material.GetFloat("_OffsetNoiseY");
			_material.SetFloat("_OffsetNoiseY", offsetNoise + Random.Range(-0.03f, 0.03f));
		}

		// Vertical shift
		if (doVerticalShift)
		{
			float offsetPosY = _material.GetFloat("_OffsetPosY");
			if (offsetPosY > 0.0f)
			{
				_material.SetFloat("_OffsetPosY", offsetPosY - Random.Range(0f, offsetPosY));
			}
			else if (offsetPosY < 0.0f)
			{
				_material.SetFloat("_OffsetPosY", offsetPosY + Random.Range(0f, -offsetPosY));
			}
			else if (Random.Range(0, 150) == 1)
			{
				_material.SetFloat("_OffsetPosY", Random.Range(-0.5f, 0.5f));
			}
		}

		// Channel color shift
		if (doColorShift)
		{
			float offsetColor = _material.GetFloat("_OffsetColor");
			if (offsetColor > colorShiftMin)
			{
				_material.SetFloat("_OffsetColor", Mathf.Lerp(offsetColor, colorShiftMin - 0.0005f, colorShiftLerp));
			}
			else if (Random.Range(0, colorShiftRand) == 1)
			{
				_material.SetFloat("_OffsetColor", Random.Range(colorShiftMin, colorShiftMax));
			}
		}

		// Distortion
		if (doDistortion)
		{
			if (Random.Range(0, distortionRand) == 1)
			{
				_material.SetFloat("_OffsetDistortion", Random.Range(disortionMin, disortionMax));
			}
			else
			{
				_material.SetFloat("_OffsetDistortion", disortionMax);
			}
		}

		// Tan Distortion
		if (doTanDistortion)
		{
			float tanOffset = _material.GetFloat("_GoTan");
			_material.SetFloat("_GoTan", Mathf.Lerp(tanOffset, tanDir, 0.05f));
			if (tanDir == 0 && Random.Range(0, 400) == 1)
			{
				tanDir = Random.value > 0.5f ? -1 : 1;
			}
			else if (Random.Range(0, 100) == 1)
			{
				tanDir = 0;
			}
		}

		Graphics.Blit(source, destination, _material);
	}
}