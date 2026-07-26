using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

	private Player player;
	private List<Collider> interactables;

	public void RegisterPlayer(Player player) {
		this.player = player;
	}

	private void OnCollisionEnter(Collision collision) {
		interactables.Add(collision.collider);
	}

	private void OnCollisionExit(Collision collision) {
		interactables.Remove(collision.collider);
	}
}
