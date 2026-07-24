using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CableInteraction : MonoBehaviour
{
    private LineRenderer activeLineRenderer;
    public float distanceFromCamera = 10f;
    private bool isFollowingHand = false;
    [SerializeField] private InputActionReference actionRef;
    [SerializeField] GameObject[] sources;
    public UnityEvent onConnectionsComplete;
    private GameObject InteractCast()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("Found an object - distance: " + hit.distance);
            return hit.collider.gameObject;
        }
        return null;
    }

    public void AttachToTarget(GameObject targetToAttach)
    {
        //This works, but allows for sources to connect to sources. If there is time, this should change
        Material targetMat = targetToAttach.GetComponent<Renderer>().material;
        Material sourceMat = activeLineRenderer.gameObject.GetComponent<Renderer>().material;
        if (targetMat.color == sourceMat.color && !targetToAttach.GetComponent<LineRenderer>())
        {
            activeLineRenderer.SetPosition(0, activeLineRenderer.gameObject.transform.position);
            activeLineRenderer.SetPosition(1, targetToAttach.transform.position);
            activeLineRenderer.gameObject.GetComponent<Collider>().enabled = false;

            if (checkColliders())
            {
                Debug.Log("Cables complete!");//TODO remove when done
                onConnectionsComplete.Invoke();
            }
        }
        else
        {
            activeLineRenderer.SetPosition(0, Vector3.zero);
            activeLineRenderer.SetPosition(1, Vector3.zero);
        }
        isFollowingHand = false;
    }

    private bool checkColliders()
    {
        for(int i = 0; i < sources.Length; i++)
        {
            if (sources[i].GetComponent<Collider>().enabled == true)
            {
                return false;
            }
            else
            {
                continue;
            }
        }
        return true;
    }

    void Update()
    {
        if (actionRef.action.WasPerformedThisFrame())
        {
            GameObject hitObject = InteractCast();
            if (hitObject != null)
            {
                LineRenderer hitLineRenderer = hitObject.GetComponent<LineRenderer>();

                if (hitLineRenderer != null)
                {
                    activeLineRenderer = hitLineRenderer;
                    isFollowingHand = true;
                }
                else if (isFollowingHand)
                {
                    AttachToTarget(hitObject);
                }
            }
        }
        if (isFollowingHand && activeLineRenderer != null)
        {
            activeLineRenderer.SetPosition(0, activeLineRenderer.gameObject.transform.position);
            Vector3 cursorPos = Mouse.current.position.ReadValue();
            cursorPos.z = distanceFromCamera;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(cursorPos);
            activeLineRenderer.SetPosition(1, mouseWorldPos);
        }
    }
}