using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClickInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference actionRef;

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

    private void Update()
    {
        if (actionRef.action.WasPerformedThisFrame())
        {
            GameObject hitObject = InteractCast();
            if (hitObject != null)
            {
                Debug.Log(hitObject.name); //TODO remove when done
            }
        }
    }
}