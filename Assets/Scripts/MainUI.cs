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
    }

    public Button pauseButton;
    public Button menuButton;
    public Button quitButton;
    public delegate void textEvent(int i);
    public static textEvent StartText;

    public Action EndText;

    public FadeableUI textOverlay;
    public FadeableUI overlay;

    public List<GameObject> kids;
    public List<GameObject> sempais;
    public MoveableText kidText;
    public MoveableText sempaiText;

    private delegate IEnumerator dialogueEvent();
    private List<dialogueEvent> dialogues;
    private dialogueEvent dialogue0;

    // Use this for initialization
    void Start() {
        pauseButton.onClick.AddListener(Pause);
        quitButton.onClick.AddListener(Quit);
        menuButton.onClick.AddListener(Menu);

        StartText = BeginText;
        EndText = FinishText;
        dialogue0 = Dialogue0;
        dialogues = new List<dialogueEvent>() { dialogue0 };
        overlay.useUnscaledDeltaTimeForUI = true;
        textOverlay.useUnscaledDeltaTimeForUI = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            BeginText(0);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            EndText();
        }
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
        yield return sempaiText.TypeText("Welcome to the team.We’ve got a rocky relationship with the coppers, so you should be aware of what you’re getting into before you start.");
        yield return new WaitForSecondsRealtime(2.0f);
        SetExpression(Expression.happy);
        yield return kidText.TypeText("Lay it on me.");
        yield return new WaitForSecondsRealtime(1.5f);
        SetExpression(Expression.neutral);
        SetExpression(Expression.listening);
        yield return sempaiText.TypeText("");
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
                break;
        }
    }
}
