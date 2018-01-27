using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBar : MonoBehaviour {

    public List<Sprite> barSprites;

    public float moveSpeed;

    public int hSteps;

    private SpriteRenderer spRenderer;

    public void SetBarSprite(float yVal) {
        if (this.spRenderer == null) {
            this.spRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (yVal < 0) {
            this.spRenderer.flipY = true;
            yVal = Mathf.Abs(yVal);
        }
        int index = Mathf.FloorToInt(yVal);
        index = Mathf.Min(index, barSprites.Count);
        this.spRenderer.sprite = barSprites[index];
    }

    void OnEnable() {
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        for (int i = 0; i < hSteps; i++) {
            this.transform.Translate(new Vector3(0.5f, 0, 0));
            yield return new WaitForSeconds(0.25f);
        }
        this.gameObject.SetActive(false);
    }
}
