﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private GameObject[] goals;
    private int numGoals;

    // Use this for initialization
    void Awake() {
        numGoals = 0;
        goals = GameObject.FindGameObjectsWithTag("Goal");
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("CopperNode")) {
            GameManager.instance.ReflectionLose();
            Destroy(this.gameObject);
        }
        if (collider.gameObject.CompareTag("Goal")) {
            ++numGoals;
        }
        if (numGoals == goals.Length) {
            ReflectionManager.instance.EndLevel();
            GameManager.instance.ReflectionWin();
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector2 force) {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
}
