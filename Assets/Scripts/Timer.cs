using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	public Ship ship;
    public Player player;
	public TextMeshProUGUI timerLabel;
	public TextMeshProUGUI stageDIrectionLabel;

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
        stageDIrectionLabel.text = "Oh no! It's falling apart!";
    }

    public void StartRound() {
        ship.SettleParts();
        player.gameObject.SetActive(true);
        player.EnableMovement();
		stageDIrectionLabel.text = "Fix it fast!";
	}

    public void HideStageDirection() {
		stageDIrectionLabel.text = "";
	}
}
