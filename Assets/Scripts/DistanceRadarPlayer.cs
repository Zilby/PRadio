using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceRadarPlayer : MonoBehaviour {

    public float curDistance;
    private float distScale = 180f;

    // Use this for initialization
    void Start () {
        StartCoroutine (SpawnRings());
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator SpawnRings() {
        while (true) {
            SpawnRing ();
            yield return new WaitForSeconds(3);
        }
    }

    void SpawnRing() {
        GameObject ring = ObjectPooler.instance.GetPooledObject ("ring");
        Debug.Log (ring);
        if (ring == null)
            return;
        ring.SetActive (true);
        ring.transform.SetParent(this.transform, false);
        ring.GetComponent<Image> ().CrossFadeAlpha (1, 0, false);
        ring.transform.localScale = Vector3.zero;
        StartCoroutine(RingExpand(ring));
    }

    IEnumerator RingExpand(GameObject ring) {
        bool fading = false;
        while (ring.transform.localScale.x <= curDistance * distScale) {
            float incr = Time.deltaTime * 20;
            ring.transform.localScale = new Vector3(ring.transform.localScale.x + incr, ring.transform.localScale.y + incr,  ring.transform.localScale.z + incr);
            if (ring.transform.localScale.x >= (curDistance * distScale * 0.8f) && !fading) {
                StartCoroutine(RingFade (ring));
            }
            yield return null;
        }
    }

    IEnumerator RingFade(GameObject ring) {

        ring.GetComponent<Image> ().CrossFadeAlpha (0, 0.5f, false);
        yield return new WaitForSeconds (0.5f);
        ring.SetActive (false);
        yield break;
    }

}
