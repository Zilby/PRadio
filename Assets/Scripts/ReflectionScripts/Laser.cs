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

	void OnCollisionEnter2D(Collision2D collider) {
		Debug.Log ("Entered");
		if (collider.gameObject.CompareTag("CopperNode")) {
			Destroy(this.gameObject);
		}
	}

}
