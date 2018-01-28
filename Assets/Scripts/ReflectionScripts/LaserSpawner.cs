using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour {

    public Laser laserPrefab;
    private Laser instance;

    public void Spawn() {
        instance = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
    }

    void Start() {
        //Spawn();
    }

	void Update () {
		if (Input.GetKeyDown ("space")) {
			Spawn ();
		}
	}
}
