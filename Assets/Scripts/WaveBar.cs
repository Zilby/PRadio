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
        int index = Mathf.FloorToInt(yVal);
        index = Mathf.Min(index, barSprites.Count);
        this.spRenderer.sprite = barSprites[index];
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = new Vector3(this.transform.position.x + moveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
    }
}
