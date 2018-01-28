using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    const float DIFFICULTY_MODIFIER = 1.2f;
    const float RISK_OFFSET = 200.0f;
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

	public delegate void pauseEvent();
	public static pauseEvent Pausing;

	public delegate IEnumerator exitEvent();
	public static exitEvent Exiting;

    private int day;

    // Risk = (reputation * distance)
    private float risk;
    // When risk > max risk player loses
    private float maxRisk;

    private float targetPopularity;
    private float popularity;

    // Reputation increases after each "day"
    private float reputation = 0.0f;

    private float listeners;

    private float timer;
    private float lastChange;
    private float timeOfSafety;

    // Use this for initialization

    void Awake() {
        Instantiate(pooler);
    }
    void Start() {
        StartCoroutine(SetupGamePhase());
		StartCoroutine(ControlGamePhase());
		mainCanvas.useUnscaledDeltaTimeForUI = true;
		Pausing = Pause;
		Exiting = ExitFade;
    }


    private IEnumerator SetupGamePhase() {
        timer = 0.0f;
        day += 1;
        this.reputation = day * DIFFICULTY_MODIFIER;
        this.maxRisk =  RISK_OFFSET / DIFFICULTY_MODIFIER;
        this.targetPopularity = reputation * POP_OFFSET / (day * DIFFICULTY_MODIFIER);
        this.board.RandomizeValues();
        board.Activated = true;
        yield return mainCanvas.FadeIn();
        sineWaveOverlay.SetActive(false);
        waveObj.SetActive(true);
    }

    private IEnumerator ControlGamePhase() {
		while (board.Activated)
		{
			timer += Time.deltaTime;
			if (timer - lastChange > changeValuesEvery)
			{
				lastChange = timer;
				this.board.RandomizeValues();
			}
			this.listeners += this.reputation * board.Distance * ((1 - board.PercentageWrong()) - 0.5f) * (1 + listeners/10);
			this.risk += (this.reputation / 4.0f) * (board.Distance - 0.5f);
			this.popularity = ((1 - board.PercentageWrong()) - 0.5f) * (1 + listeners / 10);
			listeners = Mathf.Max(0, listeners);
			risk = Mathf.Max(0, risk);
			popularity = Mathf.Max(0, popularity);
			DisplayText();

			this.sirenAudio.volume = this.risk / this.maxRisk;

			if (this.board.Overheated() || this.risk > this.maxRisk)
			{
				yield return LoseCondition();
			}
			else if (this.popularity > this.targetPopularity)
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

    private void WinCondition() {
        Pause();
        board.Activated = false;
        // Fade to black code
        // Show good job
        Debug.Log("Win!!");
        Start();
    }

    private IEnumerator LoseCondition() {
        Pause();
        board.Activated = false;
		// fade to black code
		yield return ExitFade();
		// you lose
		Debug.Log("Lose!");
        SceneManager.LoadScene("Menu");
    }

	public IEnumerator ExitFade()
	{
		sineWaveOverlay.SetActive(true);
		waveObj.SetActive(false);
		ObjectPooler.instance.DisableAllTagged("WaveBar");
		yield return mainCanvas.FadeOut();
	}
}
