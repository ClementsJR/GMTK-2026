using UnityEngine;

public class Ship : MonoBehaviour {

    public ShipPart[] parts;

	[Header("Explosion Params")]
	public float force = 1f;
	public float radius = 5f;
	public Vector3 center;

	public void ExplodeParts() {
        foreach (var part in parts) {
            part.Explode(force, center, radius);
        }
    }

    public void SettleParts() {
		foreach (var part in parts) {
			part.Settle();
		}
	}

    public bool AllReattached() {
		bool reattached = true;
		foreach (var part in parts) {
			reattached = reattached && part.IsReattached();
		}

		return reattached;
	}
}
