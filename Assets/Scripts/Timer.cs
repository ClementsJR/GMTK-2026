using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	public Ship ship;
    public Player player;
	public TextMeshProUGUI timerLabel;

	public float time = 15f;
    public List<string> eventMethods;
    public List<float> eventTimes;

    void Start() {
        UpdateTimerDisplay();
        
        for (int i = 0; i < eventMethods.Count; i++) {
            Invoke(eventMethods[i], eventTimes[i]);
        }
	}

    void Update() {
        time -= Time.deltaTime;
		UpdateTimerDisplay();
        if (time <= 0) {
            // Blast off
        }
    }

    private void UpdateTimerDisplay() {
		timerLabel.text = time.ToString("00");
	}

    public void ExplodeParts() {
        ship.ExplodeParts();
    }

    public void StartRound() {
        ship.SettleParts();
        player.EnableMovement();
    }
}
