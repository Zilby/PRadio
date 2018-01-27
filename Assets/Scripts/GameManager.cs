using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BoardController board;

    // Risk = (reputation * distance)
    private float risk;
    // When risk > max risk player loses
    private float maxRisk;

    private float targetPopularity;
    // Popularity = (reputation * listeners)
    private float popularity;

    // Reputation increases after each "day"
    public float reputation = 0.0f;

    // listeners = (distance * percentage correct signal)
    private int listeners;

    // Use this for initialization
    void Start() {
        this.SetupGamePhase();
    }

    // Update is called once per frame
    void Update() {

    }

    private void SetupGamePhase() {
        this.reputation += 1.0f;
        this.maxRisk = reputation;
        this.targetPopularity = reputation + 25.0f;
        this.board.RandomizeValues();
    }

    private void ControlGamePhase() {
        this.risk = this.reputation * this.board.Distance;
        this.listeners = Mathf.FloorToInt(this.board.PercentageCorrect() * this.board.Distance);
        this.popularity = this.reputation * this.listeners;

        if (this.board.Overheated() || this.risk > this.maxRisk) {
            this.LoseCondition();
        } else if (this.popularity > this.targetPopularity) {
            this.WinCondition();
        }
    }

    private void WinCondition() {

    }

    private void LoseCondition() {

    }
}
