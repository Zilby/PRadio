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

    // Update is called once per frame
    void Update() {
        this.transform.Translate(new Vector3(this.moveSpeed * Time.deltaTime, 0, 0));
    }
}
