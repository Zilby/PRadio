using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    // Temperature = (power + (abs(targetImpedance - impedance)))
    private float maxTemperature;

    private float targetImpedance;
    private float impedance;



    private float electricalPower;

    private float distance;

    private float frequency;
    private float targetFrequency;
    public KeyValuePair<float, float> frequencyRange;

    private float amplitude;
    private float targetAmplitude;
    public KeyValuePair<float, float> amplitudeRange;

    public void RandomizeValues() {
        this.targetFrequency = Random.Range(frequencyRange.Key, frequencyRange.Value);
        this.targetAmplitude = Random.Range(amplitudeRange.Key, amplitudeRange.Value);
        this.RandomizeImpedance();
        this.maxTemperature = 100.0f; //TODO: make this a thing
    }

    public void RandomizeImpedance() {
        this.targetImpedance = Random.Range(Mathf.Min(this.targetFrequency, this.targetAmplitude), Mathf.Max(this.targetFrequency, this.targetAmplitude));
    }

    void Awake() {
        this.RandomizeValues();
    }

    public void ModifyFrequency(float amount) {
        this.frequency += amount;
        this.electricalPower += amount / 4.0f;
        this.RandomizeImpedance();
    }

    public void ModifyAmplitude(float amount) {
        this.amplitude += amount;
        this.electricalPower += amount / 4.0f;
        this.RandomizeImpedance();
    }

    public void ModifyDistance(float amount) {
        this.distance += amount;
        this.electricalPower += amount / 2.0f;
    }

    public void ModifyImpedance(float amount) {
        this.impedance += amount;
    }

    public float GetDistance() {
        return this.distance;
    }

    public float PercentageCorrect() {
        return (this.amplitude / this.targetAmplitude + this.frequency / this.targetFrequency) / 2.0f;
    }

    public bool Overheated() {
        return this.electricalPower + Mathf.Abs(this.targetImpedance - this.impedance) > this.maxTemperature;
    }
}
