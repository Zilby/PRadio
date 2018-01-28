using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {

	public Button pauseButton;
	public Button menuButton;
	public Button quitButton;

	public delegate void textEvent(int i);
	public textEvent StartText;

	public FadeableUI textOverlay;
	public FadeableUI overlay;

	// Use this for initialization
	void Start () {
		pauseButton.onClick.AddListener(Pause);
		quitButton.onClick.AddListener(Quit);
		menuButton.onClick.AddListener(Menu);

		StartText = BeginText;
		overlay.useUnscaledDeltaTimeForUI = true;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			BeginText(0);
		}
	}

	private void BeginText(int i)
	{
		GameManager.Pausing();
		pauseButton.gameObject.SetActive(false);
		textOverlay.SelfFadeIn();
		Camera.main.GetComponent<BlurOptimized>().enabled = true;
	}

	private void EndText()
	{
		GameManager.Pausing();
		pauseButton.gameObject.SetActive(false);
		textOverlay.SelfFadeIn();
		Camera.main.GetComponent<BlurOptimized>().enabled = true;
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

	/// <summary>
	/// Quits the application. 
	/// </summary>
	private void Quit()
	{
		StartCoroutine(Exit());
	}

	/// <summary>
	/// Fades out the UI before exiting the application. 
	/// </summary>
	private IEnumerator Exit()
	{
		yield return GameManager.Exiting();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	private void Menu()
	{
		StartCoroutine(GoToMenu());
	}

	private IEnumerator GoToMenu()
	{
		yield return GameManager.Exiting();
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Menu");
	}
}
