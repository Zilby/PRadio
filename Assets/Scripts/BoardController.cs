using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardController : MonoBehaviour {

    public Slider frequencySlider;
    public Slider amplitudeSlider;
    public Slider impedanceSlider;
    public Slider distanceSlider;
    public Slider temperatureSlider;
    public SineWaveSpawner waveSpawner;
    public Transform impedanceLine;

    public AudioSource staticAudio;
    public AudioSource musicAudio;
    public AudioSource tempAlarm;

    /// <summary>
    /// The current temperature. 
    /// 	Temperature increases or decreases over time depending on the value of 
    /// 	(power + (abs(targetImpedance - impedance)) + distance)
    /// </summary>
    private float temperature = 0;
    private const float MAX_TEMPERATURE = 100f;
    private const float ALARM_THRESHOLD = 90f;
    private const float COOLDOWN_RATE = 1.0f;
    private const float TEMPERATURE_CHANGE_RATE = 1.0f;

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
    public float maxFrequency;

    /// <summary>
    /// The current amplitude
    /// </summary>
    private float amplitude;
    private float targetAmplitude;
    public float maxAmplitude;

    private float power;
    private float distance;

    private float interactionCounter = 0;
    private const float INTERACTION_TIMER = 2.0f;
    private const float INTERACTION_POWER_INCREASE = 1.0f;

    private bool activated;
    public bool Activated
    {
		get
		{
			return activated;
		}
        set
        {
            activated = value;
            if (activated) {
                interactionCounter = 0;
                StartCoroutine(SetTemperature());
				StartCoroutine(ReduceInteractionPower());
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

    void Awake() {
        this.RandomizeValues();
    }

    public void Start() {
        AssignViewEvents();
        waveSpawner.amplitude = amplitude;
        waveSpawner.frequency = frequency;
        SetAudio();
		ModifyAmplitude(amplitudeSlider.value);
		ModifyFrequency(frequencySlider.value);
		ModifyDistance(distanceSlider.value);
		ModifyImpedance(impedanceSlider.value);
	}

	private void AssignViewEvents() {
        frequencySlider.onValueChanged.AddListener(ModifyFrequency);
        amplitudeSlider.onValueChanged.AddListener(ModifyAmplitude);
        impedanceSlider.onValueChanged.AddListener(ModifyImpedance);
        distanceSlider.onValueChanged.AddListener(ModifyDistance);
    }

    private void SetAudio() {
        this.staticAudio.volume = PercentageWrong();
        this.musicAudio.volume = 1 - PercentageWrong();
    }

    private IEnumerator SetTemperature() {
        while (activated) {
            yield return new WaitForSeconds(TEMPERATURE_CHANGE_RATE);
            power = distance + (Mathf.Abs(targetImpedance - impedance) / Mathf.Max(targetAmplitude, targetFrequency))
                + (interactionCounter > 0 ? INTERACTION_POWER_INCREASE : 0);
            temperature += power - COOLDOWN_RATE;
            temperature = Mathf.Max(0, temperature);
            if (temperature > ALARM_THRESHOLD) {
                tempAlarm.Play();
            } else {
                tempAlarm.Stop();
            }
            temperatureSlider.value = temperature / MAX_TEMPERATURE;
        }
    }

    private IEnumerator ReduceInteractionPower() {
        while (activated) {
            if (interactionCounter > 0) {
                float increment = 0.2f;
                yield return new WaitForSeconds(increment);
                interactionCounter -= increment;
            }
            yield return null;
        }
    }

    public void RandomizeValues() {
        Debug.Log ("Randomizing");
        RandomizeFrequency();
        RandomizeAmplitude();
        waveSpawner.targetWave.NewTarget (targetAmplitude, targetFrequency);
        this.RandomizeImpedance();
        SetAudio();
    }

    public void RandomizeFrequency() {
        this.targetFrequency = Random.Range(0.0f, maxFrequency);
    }

    public void RandomizeAmplitude() {
        this.targetAmplitude = Random.Range(0.0f, maxAmplitude);
    }

    public void RandomizeImpedance() {
        this.targetImpedance = Random.Range(Mathf.Min(this.targetFrequency, this.targetAmplitude),
            Mathf.Max(this.targetFrequency, this.targetAmplitude));
        SetImpedenceCurrentPosition();
    }

    public void ModifyFrequency(float amount) {
        this.frequency = amount * maxFrequency;
        interactionCounter = INTERACTION_TIMER;
        this.RandomizeImpedance();
        waveSpawner.frequency = frequency;
        SetAudio();
    }

    public void ModifyAmplitude(float amount) {
        this.amplitude = amount * maxAmplitude;
        interactionCounter = INTERACTION_TIMER;
        this.RandomizeImpedance();
        waveSpawner.amplitude = amplitude;
        SetAudio();
    }

    public void ModifyDistance(float amount) {
        interactionCounter = INTERACTION_TIMER;
        this.distance = amount;
    }

    public void ModifyImpedance(float amount) {
        interactionCounter = INTERACTION_TIMER;
        // Amount * the possible target range + minimum value. 
        this.impedance = (amount * ((Mathf.Max(targetFrequency, targetAmplitude)) - Mathf.Min(targetAmplitude, targetFrequency)))
            + Mathf.Min(targetAmplitude, targetFrequency);
        SetImpedenceCurrentPosition();
    }

    private void SetImpedenceCurrentPosition() {
        impedanceLine.localPosition = new Vector3(0, Mathf.Clamp((targetImpedance - impedance) * 10.0f, -27, 27), 0);
    }

    public float PercentageWrong() {
        float wrong = Mathf.Clamp((Mathf.Abs(targetAmplitude - amplitude) / targetAmplitude + Mathf.Abs(targetFrequency - frequency) / targetFrequency) / 2.0f, 0f, 1f);
        return wrong;
    }

    public bool Overheated() {
        return temperature > MAX_TEMPERATURE;
    }
}
