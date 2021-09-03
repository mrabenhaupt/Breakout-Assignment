using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers object shake when brick destroyed event fires
/// </summary>
public class CameraShake : MonoBehaviour
{
	private float shakeDuration = 0f;
	private float shakeAmount = 0.2f;
	private float decreaseFactor = 1.0f;

	private Vector3 originalPos;

	private void Start()
	{
		originalPos = transform.localPosition;
		EventManager.Instance.onBrickDestruction += TriggerShake;
	}

	private void Update()
	{
		//What: changes/shakes objects position during predefined timespan
		if (shakeDuration > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = originalPos;
		}
	}

	public void TriggerShake()
	{
		shakeDuration = 0.1f;
	}
}