using UnityEngine;
using UnityEngine.UI;

public class KnobRotator : MonoBehaviour {

	public Slider valueSlider;
	public RectTransform mask;

	private void Start()
	{
		valueSlider.onValueChanged.AddListener(SetRotation);
	}

	private void SetRotation(float value)
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, value * 180f));
		float temp = 1.1f - (0.3f * value);
		mask.pivot = new Vector2(mask.pivot.x, temp);
	}
}
