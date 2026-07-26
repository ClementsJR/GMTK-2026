using Unity.VisualScripting;
using UnityEngine;

public class ShipPart : MonoBehaviour {

	private Vector3 targetRotation;
	private Vector3 targetPosition;
	private Rigidbody physicsBody;
	private bool beingHeld = false;
	private bool inRange = false;
	private bool reattached = false;
	private Player player;

	public Collider bodyCollider;
	public Collider targetCollider;

	void Start() {
		physicsBody = GetComponentInChildren<Rigidbody>();
		targetRotation = physicsBody.transform.eulerAngles;
		targetPosition = physicsBody.transform.position;
	}

	private void FixedUpdate() {
		if (!beingHeld)
			return;

		MoveTo(player.GetHandPosition());
	}

	private void MoveTo(Vector3 newPosition) {
		physicsBody.transform.position = newPosition;
	}

	public void Explode(float blastForce, Vector3 blastCenter, float blastRadius) {
		Vector3 randomAdjustment = Random.onUnitSphere * 0.25f;
		physicsBody.isKinematic = false;
		physicsBody.AddExplosionForce(blastForce, blastCenter+randomAdjustment, blastRadius);
		bodyCollider.enabled = true;
	}

	public void Settle() {
		physicsBody.ResetInertiaTensor();
	}

	public void PickUp(Player player) {
		this.player = player;
		beingHeld = true;
		physicsBody.isKinematic = true;
		bodyCollider.enabled = false;
		targetCollider.enabled = true;

		physicsBody.transform.eulerAngles = targetRotation;
	}

	public void Drop() {
		beingHeld = false;
		targetCollider.enabled = false;

		if (!inRange) {
			physicsBody.isKinematic = false;
			bodyCollider.enabled = true;
		} else {
			physicsBody.transform.position = targetPosition;
			reattached = true;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == player.gameObject)  inRange = true;
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject == player.gameObject) inRange = false;
	}

	public bool IsReattached() {
		return reattached;
	}
}
