using System;
using UnityEngine;
using UnityEngine.UI;

public class KnobRotator : MonoBehaviour {

	public Slider valueSlider;

	private void Start()
	{
		valueSlider.onValueChanged.AddListener(SetRotation);
	}

	private void SetRotation(float value)
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, value * 180f - 90f));
	}
}
