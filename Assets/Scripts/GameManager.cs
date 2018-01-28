using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    const float DIFFICULTY_MODIFIER = 1.2f;
    const float RISK_OFFSET = 1000.0f;
    const float POP_OFFSET = 2000.0f;

    public ObjectPooler pooler;
    public BoardController board;
    public TextMeshProUGUI listenersText;
    public TextMeshProUGUI riskText;
    public TextMeshProUGUI popText;
    public AudioSource sirenAudio;
    public float changeValuesEvery;
    public GameObject sineWaveOverlay;
    public GameObject waveObj;
    public FadeableUI mainCanvas;

    private int day;

    // Risk = (reputation * distance)
    private float risk;
    // When risk > max risk player loses
    private float maxRisk;

    private float targetPopularity;
    // Popularity = (reputation * listeners)
    private float popularity;

    // Reputation increases after each "day"
    private float reputation = 0.0f;

    // listeners = (distance * percentage correct signal)
    private int listeners;

    private float timer;
    private float lastChange;
    private float timeOfSafety;

    // Use this for initialization

    void Awake() {
        Instantiate(pooler);
    }
    void Start() {
        StartCoroutine(SetupGamePhase());
    }

    // Update is called once per frame
    void Update() {
        ControlGamePhase();
    }

    private IEnumerator SetupGamePhase() {
        timer = 0.0f;
        day += 1;
        this.reputation = day * DIFFICULTY_MODIFIER;
        this.maxRisk = reputation * RISK_OFFSET / (day * DIFFICULTY_MODIFIER);
        this.targetPopularity = reputation * POP_OFFSET / (day * DIFFICULTY_MODIFIER);
        this.board.RandomizeValues();
        board.Activated = true;
        yield return mainCanvas.FadeIn();
        sineWaveOverlay.SetActive(false);
        waveObj.SetActive(true);
    }

    private void ControlGamePhase() {
        timer += Time.deltaTime;
        if (timer - lastChange > changeValuesEvery) {
            lastChange = timer;
            this.board.RandomizeValues();
        }
        if (this.board.PercentageWrong() < 0.1f) {
            timeOfSafety = timer;
        }
        float distance = 10f * this.board.Distance;
        this.risk = (this.reputation / 4.0f) * distance * (timer / 15.0f) / (timeOfSafety + 30f);
        Debug.Log(risk);
        this.listeners = Mathf.CeilToInt(this.reputation * distance * (timer / 10.0f));
        Debug.Log(listeners);
        float listenerFavor = 1 + (board.PercentageWrong() / 100f) - 0.6f;
        this.popularity = this.listeners * listenerFavor;
        Debug.Log(popularity);
        DisplayText();

        this.sirenAudio.volume = this.risk / this.maxRisk;

        if (this.board.Overheated() || this.risk > this.maxRisk) {
            this.LoseCondition();
        } else if (this.popularity > this.targetPopularity) {
            this.WinCondition();
        }
    }

    private void DisplayText() {
        listenersText.text = "Listeners " + listeners.ToString();
        popText.text = "Popularity " + popularity.ToString();
        riskText.text = "Risk " + risk.ToString();
    }

    public void Pause() {
        Time.timeScale = 0.0f;
    }

    private void WinCondition() {
        Pause();
        board.Activated = false;
        // Fade to black code
        // Show good job
        Debug.Log("Win!!");
        Start();
    }

    private void LoseCondition() {
        Pause();
        board.Activated = false;
        // fade to black code
        StartCoroutine(mainCanvas.FadeIn());
        sineWaveOverlay.SetActive(false);
        waveObj.SetActive(true);
        // you lose
        Debug.Log("Lose!");
        SceneManager.LoadScene("Menu");
    }
}
