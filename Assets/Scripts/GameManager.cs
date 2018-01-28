using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [System.NonSerialized]
    public static GameManager instance;

    const float DIFFICULTY_MODIFIER = 1.2f;
    const float RISK_OFFSET = 100.0f;
    const float POP_OFFSET = 1200.0f;

    public ObjectPooler pooler;
    public BoardController board;
    public TextMeshProUGUI listenersText;
    public TextMeshProUGUI riskText;
    public TextMeshProUGUI popText;
    public AudioSource sirenAudio;
    public AudioSource commercial;
    public AudioSource staticNoise;
    public GameObject audioManager;
    public float changeValuesEvery;
    public GameObject sineWaveOverlay;
    public GameObject waveObj;
    public FadeableUI mainCanvas;

    public delegate void pauseEvent();
    public static pauseEvent Pausing;

    public delegate IEnumerator exitEvent();
    public static exitEvent Exiting;

    public List<AudioClip> commercials;

    public Button reflectButton;

    private int day;

    // Risk = (reputation * distance)
    private float risk;
    // When risk > max risk player loses
    private float maxRisk;

    private float targetPopularity;
    private float popularity;

    // Reputation increases after each "day"
    private float reputation = 0.0f;

    private float lroc;
    private float listeners;

    // Use this for initialization

    void Awake() {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;
        Instantiate(pooler);
        Pausing = Pause;
        Exiting = ExitFade;
    }
    void Start() {
        reflectButton.gameObject.SetActive(false);
        StartCoroutine(SetupGamePhase());
        StartCoroutine(ChangeAudio());
        mainCanvas.useUnscaledDeltaTimeForUI = true;
    }


    private IEnumerator SetupGamePhase() {
        day += 1;
        if (day >= 1) {
            reflectButton.gameObject.SetActive(true);
        }
        this.reputation = day * DIFFICULTY_MODIFIER;
        this.maxRisk = RISK_OFFSET / reputation;
        this.targetPopularity = reputation * POP_OFFSET;
        changeValuesEvery -= reputation;
        changeValuesEvery = Mathf.Max(changeValuesEvery, 15f);
        this.board.waveSpawner.InitTarget ();
        this.board.RandomizeValues();

        MainUI.StartText(1);
        yield return new WaitForSecondsRealtime(5.0f);

        //play audio
        int i = Random.Range(0, commercials.Count);
        commercial.clip = commercials[i];
        commercial.Play();
        while (commercial.isPlaying) {
            yield return null;
        }
        board.Activated = true;
        yield return mainCanvas.FadeIn();
        MainUI.StartText(0);
        staticNoise.Play();
        audioManager.SetActive(true);
        sineWaveOverlay.SetActive(false);
        waveObj.SetActive(true);
        StartCoroutine(ControlGamePhase());
    }

	private IEnumerator ChangeAudio() {
        while (true) {
            this.board.RandomizeValues ();
            yield return new WaitForSeconds (changeValuesEvery);
        }
    }

    private IEnumerator ControlGamePhase() {
        while (board.Activated) {
            lroc += 1 - board.PercentageWrong() - 0.5f;
            this.listeners = this.reputation * Mathf.Pow((board.Distance + 1.0f), 2) * board.Distance * lroc;
            this.risk += (this.reputation / 4.0f) * (board.Distance - 0.5f);
            float expression = (1 - board.PercentageWrong()) - 0.5f;
            this.popularity += expression * (listeners / 10f);
            if (expression < 0) {
                for (int i = 0; i < board.kidExpressions.Count; i++) {
                    board.kidExpressions[i].SetActive(i == 2);
                }
            } else if (expression < 0.3) {
                for (int i = 0; i < board.kidExpressions.Count; i++) {
                    board.kidExpressions[i].SetActive(i == 1);
                }
            } else {
                for (int i = 0; i < board.kidExpressions.Count; i++) {
                    board.kidExpressions[i].SetActive(i == 0);
                }
            }
            listeners = Mathf.Max(0, listeners);
            risk = Mathf.Max(0, risk);
            popularity = Mathf.Max(0, popularity);
            DisplayText();

            this.sirenAudio.volume = this.risk / this.maxRisk;

			if (this.board.Overheated() || this.risk > this.maxRisk)
			{
				yield return LoseCondition();
			}
			if (this.popularity > this.targetPopularity) 
			{
                this.WinCondition();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void DisplayText() {
        listenersText.text = "Listeners\n" + Mathf.CeilToInt(listeners).ToString();
        popText.text = "Popularity\n" + popularity.ToString();
        riskText.text = "Risk\n" + risk.ToString();
    }

    private void Pause() {
        Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
    }

    public void ReflectionWin() {
        risk = 0f;
    }

    public void ReflectionLose() {
        risk += 1f;
    }

    private void WinCondition() {
        Pause();
        board.Activated = false;
		StartCoroutine(ExitFade());
		MainUI.Win();
        // Fade to black code
        // Show good job
        Debug.Log("Win!!");
    }

    private IEnumerator LoseCondition() {
        Pause();
        board.Activated = false;
        // fade to black code
        yield return ExitFade();
		MainUI.End();
		yield return new WaitForSecondsRealtime(3.0f);
		// you lose
		Debug.Log("Lose!");
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator ExitFade() {
        sineWaveOverlay.SetActive(true);
        waveObj.SetActive(false);
        ObjectPooler.instance.DisableAllTagged("WaveBar");
        yield return mainCanvas.FadeOut();
    }
}
