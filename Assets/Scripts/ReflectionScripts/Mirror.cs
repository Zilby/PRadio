using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    void OnMouseDown() {
        transform.Rotate(0, 0, 90);
    }
}
