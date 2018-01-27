using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBar : MonoBehaviour {

    public List<Sprite> barSprites;

    public float stepSize;
    private int hSteps;
	private float stepTime;

    private SpriteRenderer spRenderer;

    public void SetBarSprite(float yVal) {
        if (this.spRenderer == null) {
            this.spRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (yVal < 0) {
            this.spRenderer.flipY = true;
            yVal = Mathf.Abs(yVal);
        }
        int index = Mathf.CeilToInt(yVal);
        index = Mathf.Min(index, barSprites.Count);
        this.spRenderer.sprite = barSprites[index];
    }

	public WaveBar Init(int hSteps, float stepTime) {
		this.hSteps = hSteps;
		this.stepTime = stepTime;
		return this;
	}

    void OnEnable() {
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        for (int i = 0; i < hSteps; i++) {
			this.transform.Translate(new Vector3(stepSize, 0, 0));
			yield return new WaitForSeconds(stepTime);
        }
        this.gameObject.SetActive(false);
    }
}
