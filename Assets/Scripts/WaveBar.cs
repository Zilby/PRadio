using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBar : MonoBehaviour {

    public List<Sprite> barSprites;

    public float moveSpeed;

    private SpriteRenderer spRenderer;

    public void SetBarSprite(float yVal) {
        if (this.spRenderer == null) {
            this.spRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        this.spRenderer.sprite = barSprites[0];
    }

    void Start() {
		StartCoroutine (Move ());
    }

	IEnumerator Move() {
		while (this.gameObject.activeSelf) {
			this.transform.Translate (new Vector3 (0.5f, 0, 0));
			yield return new WaitForSeconds (0.25f);
		}
	}
}
