using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the menu screen. 
/// </summary>
public class MenuManager : MonoBehaviour {

	public Button play;
	public Button credits;
	public Button quit;
	public Button main;
	public FadeableUI mainMenu;
	public FadeableUI creditsMenu;

	private void Start()
	{
		AssignViewEvents();
		mainMenu.Hide();
		mainMenu.SelfFadeIn();
	}

	private void AssignViewEvents()
	{
		play.onClick.AddListener(Play);
		credits.onClick.AddListener(Credits);
		quit.onClick.AddListener(Quit);
		main.onClick.AddListener(MainMenu);
	}

	private void Play()
	{
		StartCoroutine(StartGame());
	}

	private void Credits()
	{
		StartCoroutine(SwapScreens(mainMenu, creditsMenu));
	}


	private void MainMenu()
	{
		StartCoroutine(SwapScreens(creditsMenu, mainMenu));
	}


	private IEnumerator StartGame()
	{
		yield return mainMenu.FadeOut();
		SceneManager.LoadScene("Main");
	}


	private IEnumerator SwapScreens(FadeableUI fout, FadeableUI fin)
	{
		yield return fout.FadeOut();
		yield return fin.FadeIn();
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
		yield return mainMenu.FadeOut();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
