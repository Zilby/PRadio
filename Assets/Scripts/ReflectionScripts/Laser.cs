using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Vector2 momentum;
	private GameObject[] goals;
	private int numGoals;

    // Use this for initialization
    void Awake() {
        gameObject.GetComponent<Rigidbody2D>().AddForce(momentum);
		numGoals = 0;
		goals = GameObject.FindGameObjectsWithTag ("Goal");
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

	void OnCollisionEnter2D(Collision2D collider) {
		//Debug.Log ("Entered");
		if (collider.gameObject.CompareTag("CopperNode")) {
			Debug.Log ("Copper");
			Destroy(this.gameObject);
		}
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag ("Goal")) {
			Debug.Log ("Hit Goal");
			++numGoals;
		}
		if (numGoals == goals.Length) {
			//Win
			Debug.Log("Win");
			Destroy (this.gameObject);
		}
	}

	public void setDirection(Vector2 force) {
		momentum = force;
		gameObject.GetComponent<Rigidbody2D> ().AddForce (momentum);
	}


}
