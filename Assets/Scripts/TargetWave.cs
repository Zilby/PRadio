using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWave : MonoBehaviour {

    private SineWaveSpawner spawner;
    public int resolution = 63;

    public float xSeperation;
    public float yScale = .075f;

    public float baseX = -7.32f;
    public float baseY = 4.69f;


    public void Init(SineWaveSpawner sp, float barWidth) {
        spawner = sp;
        xSeperation = barWidth * 0.45f;
    }

    public void NewTarget(float amp, float f) {

        LineRenderer line = gameObject.GetComponent<LineRenderer> ();
        line.positionCount = resolution;
        for (int i = 0; i < resolution; i++) { 
            line.SetPosition (i, new Vector3 (i * xSeperation + baseX, yScale * amp * Mathf.Sin(i * f * 0.015f) + baseY, 0));
        }
    }

    //void OnEnable() {
   // 
   //    StartCoroutine (Refresh ());
   // }

    //IEnumerator Refresh() {
     //   while (true) {
     //       NewTarget ();
     //       yield return new WaitForSeconds (1);
     //   }
    //}
}
