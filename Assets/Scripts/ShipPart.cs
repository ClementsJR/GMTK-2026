using UnityEngine;

public class ShipPart : MonoBehaviour {

	private Vector3 baseRotation;
	private Rigidbody physicsBody;

	void Start() {
		baseRotation = transform.eulerAngles;
		physicsBody = GetComponent<Rigidbody>();
	}

	public void Explode(float blastForce, Vector3 blastCenter, float blastRadius) {
		physicsBody.isKinematic = false;
		physicsBody.AddExplosionForce(blastForce, blastCenter, blastRadius);
	}
}
