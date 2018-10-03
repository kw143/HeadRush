using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class TVShader : MonoBehaviour
{
	public Material _material;

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
		_material.SetFloat("_OffsetNoiseX", Random.Range(0f, 0.6f));
		float offsetNoise = _material.GetFloat("_OffsetNoiseY");
		_material.SetFloat("_OffsetNoiseY", offsetNoise + Random.Range(-0.03f, 0.03f));

		// Vertical shift
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

		// Channel color shift
		float offsetColor = _material.GetFloat("_OffsetColor");
		if (offsetColor > 0.003f)
		{
			_material.SetFloat("_OffsetColor", Mathf.Lerp(offsetColor, 0.0025f, 0.05f));
		}
		else if (Random.Range(0, 600) == 1)
		{
			_material.SetFloat("_OffsetColor", Random.Range(0.003f, 0.05f));
		}

		// Distortion
		if (Random.Range(0, 15) == 1)
		{
			_material.SetFloat("_OffsetDistortion", Random.Range(300f, 480f));
		}
		else
		{
			_material.SetFloat("_OffsetDistortion", 480f);
		}

		// Tan Distortion
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

		Graphics.Blit(source, destination, _material);
	}
}