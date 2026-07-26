using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool acceptInput = false;
	private bool holdingPart = false;
	private InteractableDetector detector;

	private void Start() {
		detector = GetComponentInChildren<InteractableDetector>();
		detector.RegisterPlayer(this);
	}

	public void EnableMovement() {
        acceptInput = true;
	}

	public void DisableMovement() {
		acceptInput = false;
	}

	public void Move() {
	}

	public void Interact() {

	}
}
