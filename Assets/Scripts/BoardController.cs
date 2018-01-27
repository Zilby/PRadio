using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {

	public Slider frequencySlider;
	public Slider amplitudeSlider;
	public Slider impedanceSlider;
	public Slider distanceSlider;

	/// <summary>
	/// The current temperature. 
	/// 	Temperature increases or decreases over time depending on the value of 
	/// 	(power + (abs(targetImpedance - impedance)) + distance)
	/// </summary>
	private float temperature = 0;
    private const float MAX_TEMPERATURE = 100f;
	private const float COOLDOWN_RATE = 0.5f;
	private const float TEMPERATURE_CHANGE_RATE = 1.0F;

	/// <summary>
	/// The current impedance
	/// </summary>
    private float impedance;
	private float targetImpedance;

	/// <summary>
	/// The current frequency
	/// </summary>
	private float frequency;
	private float targetFrequency;
	public KeyValuePair<float, float> frequencyRange;

	/// <summary>
	/// The current amplitude
	/// </summary>
    private float amplitude;
	private float targetAmplitude;
	public KeyValuePair<float, float> amplitudeRange;

	private float power;
	private float distance;

	private float interactionCounter = 0;
	private const float INTERACTION_TIMER = 2.0f;
	private const float INTERACTION_POWER_INCREASE = 1.0f;

	private bool activated;
	public bool Activated
	{
		set
		{
			activated = value;
			if (activated)
			{
				interactionCounter = 0;
				StartCoroutine(SetTemperature());
			}
		}
	}

	private float Power
	{
		get { return power; }
	}

	public float Distance
	{
		get { return distance; }
	}

	public void Start()
	{
		StartCoroutine(ReduceInteractionPower());
	}

	private void AssignViewEvents()
	{
		frequencySlider.onValueChanged.AddListener(ModifyFrequency);
		amplitudeSlider.onValueChanged.AddListener(ModifyAmplitude);
		impedanceSlider.onValueChanged.AddListener(ModifyImpedance);
		distanceSlider.onValueChanged.AddListener(ModifyDistance);
	}

	private IEnumerator SetTemperature()
	{
		while(activated)
		{
			yield return new WaitForSeconds(TEMPERATURE_CHANGE_RATE);
			power = distance + (Mathf.Abs(targetImpedance - impedance))  / Mathf.Max(amplitudeRange.Value, frequencyRange.Value)
				+ (interactionCounter > 0 ? INTERACTION_POWER_INCREASE : 0);
			temperature += power - 0.5f;
			temperature = Mathf.Max(0, temperature);
		}
	}

	private IEnumerator ReduceInteractionPower()
	{
		while(activated)
		{
			if (interactionCounter > 0)
			{
				float increment = 0.2f;
				yield return new WaitForSeconds(increment);
				interactionCounter -= increment;
			}
			yield return null;
		}
	}

	public void RandomizeValues() {
		RandomizeFrequency();
		RandomizeAmplitude();
        this.RandomizeImpedance();
    }

	public void RandomizeFrequency()
	{
		this.targetFrequency = Random.Range(frequencyRange.Key, frequencyRange.Value);
	}

	public void RandomizeAmplitude()
	{
		this.targetAmplitude = Random.Range(amplitudeRange.Key, amplitudeRange.Value);
	}

	public void RandomizeImpedance() {
        this.targetImpedance = Random.Range(Mathf.Min(this.targetFrequency, this.targetAmplitude), 
			Mathf.Max(this.targetFrequency, this.targetAmplitude));
    }

    void Awake() {
        this.RandomizeValues();
    }

    public void ModifyFrequency(float amount) {
        this.frequency += amount;
		interactionCounter = INTERACTION_TIMER;
        this.RandomizeImpedance();
    }

    public void ModifyAmplitude(float amount) {
        this.amplitude += amount;
		interactionCounter = INTERACTION_TIMER;
        this.RandomizeImpedance();
    }

    public void ModifyDistance(float amount) {
		interactionCounter = INTERACTION_TIMER;
		this.distance += amount;
    }

    public void ModifyImpedance(float amount) {
		interactionCounter = INTERACTION_TIMER;
		this.impedance += amount;
    }

    public float PercentageCorrect() {
        return (this.amplitude / this.targetAmplitude + this.frequency / this.targetFrequency) / 2.0f;
    }

    public bool Overheated() {
        return temperature > MAX_TEMPERATURE;
    }
}
