using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWave : MonoBehaviour {

	// This is essentially cached, don't trust it
	public float amplitude;

	public float frequency;
	
	public float phase;

	public float SinFunction(float xVal) {
		return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * xVal + phase);
	}
}
