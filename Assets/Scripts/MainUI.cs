using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using System;

public class MainUI : MonoBehaviour {

    public enum Expression {
        happy,
        neutral,
        meh,
        listening,
        pointing,
        noKid,
    }

    public Button pauseButton;
    public Button menuButton;
    public Button quitButton;

    public delegate void textEvent(int i);
    public static textEvent StartText;

    public static Action EndText;
	public static Action Win;
	public static Action End;

    public FadeableUI textOverlay;
    public FadeableUI overlay;

    public List<GameObject> kids;
    public List<GameObject> sempais;
    public MoveableText kidText;
    public MoveableText sempaiText;

	public FadeableUI win;
	public FadeableUI lose;

    private delegate IEnumerator dialogueEvent();
    private List<dialogueEvent> dialogues;
    private dialogueEvent dialogue0, dialogue1, dialogue2, dialogue3;

    // Use this for initialization
    void Awake() {
        pauseButton.onClick.AddListener(Pause);
        quitButton.onClick.AddListener(Quit);
        menuButton.onClick.AddListener(Menu);

        StartText = BeginText;
        EndText = FinishText;
		Win = WinGame;
		End = EndGame;
        dialogue0 = Dialogue0;
        dialogue1 = Dialogue1;
        dialogues = new List<dialogueEvent>() { dialogue0, dialogue1, dialogue2, dialogue3 };
        overlay.useUnscaledDeltaTimeForUI = true;
        textOverlay.useUnscaledDeltaTimeForUI = true;
    }

    private void BeginText(int i) {
        GameManager.Pausing();
        pauseButton.gameObject.SetActive(false);
        textOverlay.SelfFadeIn();
        Camera.main.GetComponent<BlurOptimized>().enabled = true;
        sempaiText.ClearText();
        kidText.ClearText();
        StartCoroutine(dialogues[i]());
    }

    private void FinishText() {
        GameManager.Pausing();
        pauseButton.gameObject.SetActive(true);
        textOverlay.SelfFadeOut();
        Camera.main.GetComponent<BlurOptimized>().enabled = false;
    }


    private IEnumerator Dialogue0() {
        SetExpression(Expression.neutral);
        SetExpression(Expression.listening);
        yield return new WaitForSecondsRealtime(0.5f);
        yield return sempaiText.TypeText("Welcome to the team. We’ve got a rocky relationship with the coppers, so you should be aware of what you’re getting into before you start");
        yield return new WaitForSecondsRealtime(2.0f);
        sempaiText.ClearText();
        SetExpression(Expression.happy);
        yield return kidText.TypeText("Lay it on me");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.neutral);
        SetExpression(Expression.listening);
        kidText.ClearText();
        yield return sempaiText.TypeText("Alright, I’ll give you the rundown. As you already know, Resist and Transmit is an underground rebel group that’s trying to bring music back to the radio.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("We started up in 3018, when the radio was sold to corporations and they stopped playing music and started running ads 24/7.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("Bastard corporations. So what we do is take over the radio and play music over their commercials");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.pointing);
        yield return sempaiText.TypeText("That being said, I think it’s about time to stick it to the man. Here’s how it works");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.listening);
        yield return sempaiText.TypeText("There’s a visualizer at the top of the board. See it? Doesn’t matter. That’s the wavelength we’re trying to match.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("In order to take over the station, we’ve gotta make sure that our frequency and amplitude line up with it. Use the dials to adjust how wide and how tall our wavelength is");
        yield return new WaitForSecondsRealtime(1.5f);
        sempaiText.ClearText();
        yield return kidText.TypeText("Uh, okay. Got it");
        yield return new WaitForSecondsRealtime(1.5f);
        kidText.ClearText();
        yield return sempaiText.TypeText("And you see that visualizer?");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.meh);
        yield return kidText.TypeText("Um… Which one?");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.neutral);
        kidText.ClearText();
        yield return sempaiText.TypeText("Eh, you’ll figure it out. Anyway, similar idea. That controls something called impedance.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("Long story short, what you’ve gotta do is use that slider right there to make sure that the red horizontal line you see there matches up with the white horizontal line.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("That’s gonna ensure that we’re not sending out standing waves so that our systems don’t overheat. Got it?");
        yield return new WaitForSecondsRealtime(1.5f);
        sempaiText.ClearText();
        SetExpression(Expression.meh);
        yield return kidText.TypeText("I… Think so?");
        yield return new WaitForSecondsRealtime(1.5f);
        kidText.ClearText();
        yield return sempaiText.TypeText("And there’s a slider at the bottom. That one controls the radius of our broadcast. So the higher that slider is, the more people we’re reaching.");
        yield return new WaitForSecondsRealtime(1.5f);
        yield return sempaiText.TypeText("It’ll increase our popularity, but that also means that the chances that the coppers will be able to find us increases. So use it wisely. Got any questions?");
        yield return new WaitForSecondsRealtime(1.5f);
        sempaiText.ClearText();
        yield return kidText.TypeText("Uh, yeah, so—");
        yield return new WaitForSecondsRealtime(1.5f);
        kidText.ClearText();
        SetExpression(Expression.pointing);
        yield return sempaiText.TypeText("Yoinkers! I gotta be gettin’ out of here. The husband’ll be wondering where I am. Good luck, kid. You’ll figure this out");
        yield return new WaitForSecondsRealtime(2.0f);

        FinishText();
    }


    private IEnumerator Dialogue1() {
        SetExpression(Expression.noKid);
        SetExpression(Expression.listening);
        yield return new WaitForSecondsRealtime(4.0f);
        SetExpression(Expression.pointing);
        yield return sempaiText.TypeText("You ready for this, kid?");
        yield return new WaitForSecondsRealtime(2.0f);

        FinishText();
    }


    private void Pause() {
        GameManager.Pausing();
        if (Time.timeScale == 0.0f) {
            overlay.SelfFadeIn();
        } else {
            overlay.SelfFadeOut();
        }
        Camera.main.GetComponent<BlurOptimized>().enabled = Time.timeScale == 0.0f;
    }

    /// <summary>
    /// Quits the application. 
    /// </summary>
    private void Quit() {
        StartCoroutine(Exit());
    }

    /// <summary>
    /// Fades out the UI before exiting the application. 
    /// </summary>
    private IEnumerator Exit() {
        yield return GameManager.Exiting();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Menu() {
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu() {
        yield return GameManager.Exiting();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    private void SetExpression(Expression e) {
        switch (e) {

            case Expression.happy:
                for (int i = 0; i < kids.Count; ++i) {
                    kids[i].SetActive(i == 0);
                }
                break;
            case Expression.neutral:
                for (int i = 0; i < kids.Count; ++i) {
                    kids[i].SetActive(i == 1);
                }
                break;
            case Expression.meh:
                for (int i = 0; i < kids.Count; ++i) {
                    kids[i].SetActive(i == 2);
                }
                break;
            case Expression.listening:
                for (int i = 0; i < sempais.Count; ++i) {
                    sempais[i].SetActive(i == 0);
                }
                break;
            case Expression.pointing:
                for (int i = 0; i < sempais.Count; ++i) {
                    sempais[i].SetActive(i == 1);
                }
                break;
            default:
                for (int i = 0; i < kids.Count; ++i) {
                    kids[i].SetActive(false);
                }
                break;
        }
    }

	private void WinGame()
	{
		StartCoroutine(ActualWinGame());
		

	}


	private IEnumerator ActualWinGame()
	{
		win.useUnscaledDeltaTimeForUI = true;
		yield return win.FadeIn();
		yield return new WaitForSecondsRealtime(4.0f);
		yield return win.FadeOut();
		//SceneManager.LoadScene("Menu");
	}


	private void EndGame()
	{
		lose.useUnscaledDeltaTimeForUI = true;
		lose.SelfFadeIn();
	}
}
