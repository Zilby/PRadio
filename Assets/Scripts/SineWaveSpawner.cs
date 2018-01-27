using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveSpawner : MonoBehaviour {

    public float cappedAmplitude;
	public int numBars;

    // This is essentially cached, don't trust it
    public float amplitude;
    public float frequency;

    public float spawnTime;

    private bool paused = false;

	private float curX = 0;
    private float baseFrequency = 0.015f;
	private float vOffset = 1.9f;

    public float SinFunction(float x) {
        if (amplitude > cappedAmplitude) {
            amplitude = cappedAmplitude;
        }
        return amplitude * Mathf.Sin(frequency * x);
    }

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (!paused) {
            yield return new WaitForSeconds(spawnTime);
			this.curX += this.baseFrequency;
            float sin = this.SinFunction(this.curX);
			subSpawn(sin, 1);
			subSpawn(sin, -1);
        }
    }

	void subSpawn(float sin,  int flip) {
		if (sin * flip < 0) {
			sin = 0;  
		}
		GameObject bar = ObjectPooler.instance.GetPooledObjectAtPosition("WaveBar", new Vector3(this.transform.position.x, this.transform.position.y+(vOffset * flip), this.transform.position.z), Quaternion.identity);
		if (bar != null) {
			bar.GetComponent<WaveBar>().Init(numBars, spawnTime).SetBarSprite(sin);
			bar.gameObject.SetActive(true);
		}
	}
}
