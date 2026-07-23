using UnityEngine;

public class ShipPart : MonoBehaviour {

	private Vector3 baseRotation;
	private Rigidbody physicsBody;

	void Start() {
		baseRotation = transform.eulerAngles;
		physicsBody = GetComponent<Rigidbody>();
	}

	public void Explode(float blastForce, Vector3 blastCenter, float blastRadius) {
		Vector3 randomAdjustment = Random.onUnitSphere * 0.25f;
		physicsBody.isKinematic = false;
		physicsBody.AddExplosionForce(blastForce, blastCenter+randomAdjustment, blastRadius);
	}

	public void Settle() {
		physicsBody.ResetInertiaTensor();
	}
}
