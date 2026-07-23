using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float time = 20f;
    public float timeToExplosion = 5f;
    public float workTime = 10f;
    private bool exploded = false;
    private bool partsSettled = false;

    public Ship ship;
    public TextMeshProUGUI timerLabel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        UpdateTimerDisplay();

	}

    void Update() {
        time -= Time.deltaTime;
		UpdateTimerDisplay();
        if (time <= 0) {
            // Blast off
        }

        if (time <= workTime && !partsSettled) {
            partsSettled = true;
            ship.SettleParts();
        }

        timeToExplosion -= Time.deltaTime;
        if (timeToExplosion <= 0 && !exploded) {
            ship.ExplodeParts();
            exploded = true;
        }
    }

    private void UpdateTimerDisplay() {
		timerLabel.text = time.ToString("00");
	}
}
