using UnityEngine;
using UnityEngine.InputSystem;

public class CableInteraction : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float distanceFromCamera = 10f;
    private bool isFollowingHand = false;
    
    public void toggleFollowHand()
    {
        isFollowingHand = !isFollowingHand;
    }

    public void AttachToTarget(GameObject targatToAttach)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targatToAttach.transform.position);
    }
    
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isFollowingHand)
        {
            lineRenderer.SetPosition(0, transform.position);
            Vector3 cursorPos = Mouse.current.position.ReadValue();
            cursorPos.z = distanceFromCamera;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(cursorPos);
            lineRenderer.SetPosition(1, mouseWorldPos);
        }
        else
        {
            return;
        }
    }
}
