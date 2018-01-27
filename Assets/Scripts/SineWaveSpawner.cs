﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveSpawner : MonoBehaviour {

    public float cappedAmplitude;

    // This is essentially cached, don't trust it
    public float amplitude;

    public float frequency;

    public float phase;

    public float spawnFrequency;

    private bool paused = false;

    public float SinFunction(float time) {
        if (amplitude > cappedAmplitude) {
            amplitude = cappedAmplitude;
        }
        return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time + phase);
    }

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (!paused) {
            yield return new WaitForSeconds(spawnFrequency);
            float sin = this.SinFunction(Time.time);
            GameObject bar = ObjectPooler.instance.GetPooledObjectAtPosition("WaveBar", this.transform.position, Quaternion.identity);
            if (bar != null) {
                bar.gameObject.SetActive(true);
                if (sin > 0) {
                    bar.GetComponent<WaveBar>().SetBarSprite(sin);
                } else {
                    bar.GetComponent<WaveBar>().SetBarSprite(0);
                }
            }
            GameObject bar2 = ObjectPooler.instance.GetPooledObjectAtPosition("WaveBar", new Vector3(this.transform.position.x, this.transform.position.y - 6.4f, this.transform.position.z), Quaternion.Euler(0, 0, 180));
            if (bar2 != null) {
                bar2.gameObject.SetActive(true);
                if (sin < 0) {
                    bar2.GetComponent<WaveBar>().SetBarSprite(Mathf.Abs(sin));
                } else {
                    bar2.GetComponent<WaveBar>().SetBarSprite(0);
                }
            }
        }
    }
}
