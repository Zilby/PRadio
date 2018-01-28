using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainUI : MonoBehaviour {

	public Button pauseButton;
	public FadeableUI overlay;

	// Use this for initialization
	void Start () {
		pauseButton.onClick.AddListener(Pause);
		overlay.useUnscaledDeltaTimeForUI = true;
	}

	private void Pause()
	{
		GameManager.Pausing();
		if(Time.timeScale == 0.0f)
		{
			overlay.SelfFadeIn();
		}
		else
		{
			overlay.SelfFadeOut();
		}
		Camera.main.GetComponent<BlurOptimized>().enabled = Time.timeScale == 0.0f;
	}
}
