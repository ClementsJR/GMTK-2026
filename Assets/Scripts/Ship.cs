using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public ShipPart[] allParts;
	private List<ShipPart> detachedParts = new List<ShipPart>();

	[Header("Explosion Params")]
	public float force = 1f;
	public float radius = 5f;
	public Vector3 center;

	private void Start() {
		var difficulty = 1;

		for(int i = 0; i < difficulty; i++) {
			detachedParts.Add(allParts[i]);
		}
	}

	public void ExplodeParts() {
        foreach (var part in detachedParts) {
            part.Explode(force, center, radius);
        }
    }

    public void SettleParts() {
		foreach (var part in detachedParts) {
			part.Settle();
		}
	}

    public bool AllReattached() {
		bool reattached = true;
		foreach (var part in detachedParts) {
			reattached = reattached && part.IsReattached();
		}

		return reattached;
	}
}
