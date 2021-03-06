﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveSpawner : MonoBehaviour {

    public TargetWave targetWave;

    public float cappedAmplitude;
    public int numBars;

    // This is essentially cached, don't trust it
    [System.NonSerialized]
    public float amplitude;
    [System.NonSerialized]
    public float frequency;

    public float spawnTime;

    private bool paused = false;

    private float curX = 0;
    private float baseFrequency = 0.015f;
    private float vOffset = 1.9f;
    private float barWidth = 0.4f;

    public float SinFunction(float x) {
        if (amplitude > cappedAmplitude) {
            amplitude = cappedAmplitude;
        }
        return amplitude * Mathf.Sin(x);
    }

    void Start() {
        for (int i = 0; i < numBars; i++) {
            spawnBar(0, 1, i);
            spawnBar(0, -1, i);
        }

        StartCoroutine(SpawnLoop());
    }

    public IEnumerator SpawnLoop() {
        while (!paused) {
            yield return new WaitForSeconds(spawnTime);
            this.curX += this.baseFrequency * frequency;
            float sin = this.SinFunction(this.curX);
            spawnBar(sin, 1, 0);
            spawnBar(sin, -1, 0);
        }
    }

    void spawnBar(float sin, int flip, int spawnPos) {
        if (sin * flip < 0) {
            sin = 0;
        }
        GameObject bar = ObjectPooler.instance.GetPooledObjectAtPosition("WaveBar", new Vector3(this.transform.position.x + (spawnPos * barWidth * this.transform.lossyScale.x), this.transform.position.y + (vOffset * flip * this.transform.lossyScale.y), this.transform.position.z), Quaternion.identity);
        if (bar != null) {
            bar.GetComponent<WaveBar>().Init(numBars - spawnPos, spawnTime, barWidth * this.transform.lossyScale.x).SetBarSprite(sin);
            bar.transform.localScale = new Vector3(this.transform.lossyScale.x * 0.65f, this.transform.lossyScale.y, this.transform.lossyScale.z);
            bar.gameObject.SetActive(true);
        }
    }

    public void InitTarget() {
        targetWave.Init (this, barWidth);
    }

}
