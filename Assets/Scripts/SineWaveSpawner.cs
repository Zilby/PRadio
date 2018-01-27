using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveSpawner : MonoBehaviour {

    // This is essentially cached, don't trust it
    public float amplitude;

    public float frequency;

    public float phase;

    public float spawnFrequency;

    private bool paused = false;

    public float SinFunction(float time) {
        return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time + phase);
    }

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (!paused) {
            yield return new WaitForSeconds(spawnFrequency);
            GameObject bar = ObjectPooler.instance.GetPooledObjectAtPosition("WaveBar", this.transform.position, Quaternion.identity);
            if (bar != null) {
                bar.gameObject.SetActive(true);
                bar.GetComponent<WaveBar>().SetBarSprite(this.SinFunction(Time.time));
            }
        }
    }
}
