using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour {

    public Laser laserPrefab;
    private Laser instance;

    public void Spawn() {
        instance = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
		if (this.transform.eulerAngles.z == 0) {
			instance.setDirection(new Vector2 (200, 0));
		}
		if (this.transform.eulerAngles.z == 90) {
			instance.setDirection(new Vector2 (0, 200));
		}
		if (this.transform.eulerAngles.z == 180) {
			instance.setDirection(new Vector2 (-200, 0));
		}
		if (this.transform.eulerAngles.z == 270) {
			instance.setDirection(new Vector2 (0, -200));
		}
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
