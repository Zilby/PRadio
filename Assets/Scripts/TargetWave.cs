using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWave : MonoBehaviour {

    private SineWaveSpawner spawner;
    private int resolution = 50;

    private float xSeperation;
    private float yScale;

    private float baseX;
    private float baseY;



    void Start() {
        spawner = this.transform.parent.gameObject.GetComponent<SineWaveSpawner>();
    }

    public void Init(SineWaveSpawner sp, float barWidth) {
        spawner = sp;
        xSeperation = barWidth;
    }

    void NewTarget() {

        LineRenderer line = gameObject.GetComponent<LineRenderer> ();
        line.positionCount = resolution;
        for (int i = 0; i < resolution; i++) { 
            line.SetPosition (i, new Vector3 (i * xSeperation + baseX, yScale * spawner.SinFunction (i, true) + baseY, 0));
        }
    }
}
