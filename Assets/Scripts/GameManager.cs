using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    const float DIFFICULTY_MODIFIER = 1.2f;
    const float RISK_OFFSET = 1000.0f;
    const float POP_OFFSET = 2000.0f;

    public BoardController board;

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

    // Use this for initialization
    void Start() {
        this.SetupGamePhase();
    }

    // Update is called once per frame
    void Update() {
        ControlGamePhase();
    }

    private void SetupGamePhase() {
        day += 1;
        this.reputation = day * DIFFICULTY_MODIFIER;
        this.maxRisk = reputation * RISK_OFFSET / (day * DIFFICULTY_MODIFIER);
        this.targetPopularity = reputation * POP_OFFSET / (day * DIFFICULTY_MODIFIER);
        this.board.RandomizeValues();
        // activate board
    }

    private void ControlGamePhase() {
        this.risk = this.reputation * this.board.GetDistance();
        this.listeners = Mathf.FloorToInt(this.board.PercentageCorrect() * this.board.GetDistance());
        this.popularity = this.reputation * this.listeners;

        if (this.board.Overheated() || this.risk > this.maxRisk) {
            this.LoseCondition();
        } else if (this.popularity > this.targetPopularity) {
            this.WinCondition();
        }
    }

    public void Pause() {
        Time.timeScale = 0.0f;
    }

    private void WinCondition() {
        Pause();
        // deactivate board
        // Fade to black code
        // Show good job
        Debug.Log("Win!!");
        Start();
    }

    private void LoseCondition() {
        Pause();
        // deactivate board
        // fade to black code
        // you lose
        Debug.Log("Lose!");
        SceneManager.LoadScene("Menu");
    }
}
