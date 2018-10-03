using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GradientBackground : MonoBehaviour {

	public UnityEngine.UI.RawImage img;
	public Color color1, color2;

	private Texture2D backgroundTexture;

#if UNITY_EDITOR

	void Update()
	{
		SetColor(color1, color2);
	}

#endif

	void Awake()
	{
		backgroundTexture = new Texture2D(1, 2);
		backgroundTexture.wrapMode = TextureWrapMode.Clamp;
		backgroundTexture.filterMode = FilterMode.Bilinear;
		SetColor(color1, color2);
	}

	public void SetColor(Color color1, Color color2)
	{
		backgroundTexture.SetPixels(new Color[] { color1, color2 });
		backgroundTexture.Apply();
		img.texture = backgroundTexture;
	}
}
