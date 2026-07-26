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

    private bool levelOver = false;

    void Start() {
        UpdateTimerDisplay();
        
        for (int i = 0; i < eventMethods.Count; i++) {
            Invoke(eventMethods[i], eventTimes[i]);
        }

        var difficulty = 1;
        time = (15 * difficulty) + 5;
	}

    void Update() {
        if (time <= 0 && !levelOver) {
            levelOver = true;
            player.DisableMovement();
            if (ship.AllReattached()) {
                LevelSuccess();
            } else {
                LevelFail();
            }
        } else if (time > 0) {
			time -= Time.deltaTime;
			UpdateTimerDisplay();
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

    private void LevelSuccess() {
		stageDIrectionLabel.text = "We're blasting off!";
        player.gameObject.SetActive(false);
	}

    private void LevelFail() {
		stageDIrectionLabel.text = "The ship left without us!";
	}
}
