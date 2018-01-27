using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the menu screen. 
/// </summary>
public class MenuManager : MonoBehaviour {

	public Button play;
	public Button credits;
	public Button quit;
	public FadeableUI mainMenu;

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
	}

	private void Play()
	{

	}

	private void Credits()
	{

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
