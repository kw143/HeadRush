using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateIn3D : MonoBehaviour {

	public float effectTime = 1;
	[Header("Position Animation")]
	public Vector3 positionStart;
	public AnimationCurve posAnimCurve;

	Vector3 positionEnd;

	bool animating;
	bool animDone;

	// Use this for initialization
	void Start () {
		positionEnd = transform.position;
		StartCoroutine(Animate());
	}

	IEnumerator Animate()
	{
		animating = true;
		if (animDone)
		{
			transform.position = positionEnd;
		}
		else
		{
			transform.position = positionStart;
		}
		float time = 0;
		float perc = 0;
		float lastTime = Time.realtimeSinceStartup;
		do
		{
			time += Time.realtimeSinceStartup - lastTime;
			lastTime = Time.realtimeSinceStartup;
			perc = Mathf.Clamp01(time / effectTime);
			if (animDone)
			{
				transform.position = Vector3.LerpUnclamped(positionEnd, positionStart, posAnimCurve.Evaluate(perc));
			}	
			else
			{
				transform.position = Vector3.LerpUnclamped(positionStart, positionEnd, posAnimCurve.Evaluate(perc));
			}
			yield return null;
		} while (perc < 1);
		if (animDone)
		{
			transform.position = positionStart;
		}
		else
		{
			transform.position = positionEnd;
		}
		animDone = !animDone;
		animating = false;
	}
}
