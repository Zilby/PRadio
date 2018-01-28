using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour {

    public Laser laserPrefab;
    private Laser instance;

    void Start() {
        instance = null;
    }

    public void Spawn() {
        if (instance == null) {
            instance = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
            instance.SetDirection(new Vector2(-200, 0));
        }
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            Spawn();
        }
    }
}
