using UnityEngine;

public class Ship : MonoBehaviour {

    public ShipPart[] parts;

	[Header("Explosion Params")]
	public float force = 1f;
	public float radius = 5f;
	public Vector3 center;

	public void ExplodeParts() {
        if (center == null) center = transform.position;

        foreach (var part in parts) {
            part.Explode(force, center, radius);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
