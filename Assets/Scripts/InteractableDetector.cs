using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

	private Player player;
	private List<Collider> interactables;

	public void RegisterPlayer(Player player) {
		this.player = player;
	}

	public ShipPart GetInteractable() {
		var numParts = interactables.Count;
		if (numParts == 0) return null;
		else return interactables[numParts-1].GetComponentInParent<ShipPart>();
	}

	private void OnCollisionEnter(Collision collision) {
		interactables.Add(collision.collider);
	}

	private void OnCollisionExit(Collision collision) {
		interactables.Remove(collision.collider);
	}
}
