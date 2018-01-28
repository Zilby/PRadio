using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Vector2 momentum;

    // Use this for initialization
    void Awake() {
        gameObject.GetComponent<Rigidbody2D>().AddForce(momentum);
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

}
