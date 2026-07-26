using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

	private Player player;
	private List<ShipPart> interactables = new List<ShipPart>();

	public void RegisterPlayer(Player player) {
		this.player = player;
	}

	public ShipPart GetInteractable() {
		var numParts = interactables.Count;
		if (numParts == 0) return null;
		else {
			ShipPart part = interactables[numParts - 1];
			interactables.Remove(part);
			return part;
		}
	}

	private void OnTriggerEnter(Collider other) {
		ShipPart part = other.GetComponentInParent<ShipPart>();
		if (part != null) interactables.Add(part);
	}

	private void OnTriggerExit(Collider other) {
		ShipPart part = other.GetComponentInParent<ShipPart>();
		if (part != null) interactables.Remove(part);
	}
}
