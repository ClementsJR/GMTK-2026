using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClickInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference actionRef;
    
    private GameObject InteractCast()
    {
        UnityEngine.Vector2 screenPosition = Mouse.current.position.ReadValue();
        RaycastHit hit;

        if (Physics.Raycast(screenPosition, Vector3.forward, out hit, Mathf.Infinity))
        {
            Debug.Log("Found an object - distance: " + hit.distance);
        }
        return this.gameObject;
    }
    
    private void FixedUpdate()
    {
        if (actionRef.action.inProgress)
        {
            Debug.Log(InteractCast().name);//TODO remove when done

        }
    }
}