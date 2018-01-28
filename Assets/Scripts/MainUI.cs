using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainUI : MonoBehaviour {

	public Button pauseButton;

	// Use this for initialization
	void Start () {
		pauseButton.onClick.AddListener(Pause);
	}

	private void Pause()
	{
		GameManager.Pausing();
		Camera.main.GetComponent<BlurOptimized>().enabled = Time.timeScale == 0.0f;
	}
}
