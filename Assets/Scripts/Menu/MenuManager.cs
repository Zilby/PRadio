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
	public SineWaveSpawner waves;

	private void Start()
	{
		AssignViewEvents();
		mainMenu.Hide();
		mainMenu.SelfFadeIn();
		waves.amplitude = 15;
		waves.frequency = 15;
		waves.gameObject.SetActive(false);
		ObjectPooler.instance.DisableAllTagged("WaveBar");
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
		waves.gameObject.SetActive(true);
		StartCoroutine(waves.SpawnLoop());
		ObjectPooler.instance.DisableAllTagged("WaveBar");
	}


	private void MainMenu()
	{
		waves.gameObject.SetActive(false);
		ObjectPooler.instance.DisableAllTagged("WaveBar");
		StartCoroutine(SwapScreens(creditsMenu, mainMenu));
	}


	private IEnumerator StartGame()
	{
		ObjectPooler.instance.DisableAllTagged("WaveBar");
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
		waves.gameObject.SetActive(false);
		ObjectPooler.instance.DisableAllTagged("WaveBar");
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
