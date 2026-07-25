using UnityEngine;
using UnityEngine.InputSystem;

public class PuckInteraction : MonoBehaviour
{
    private bool isInHand;
    private Rigidbody2D puckRB;


    public void toggleCursorFollow()
    {
        isInHand = !isInHand;
    }
    public void FollowCursor()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 ScreenPos = new Vector3(Mathf.Lerp(puckRB.position.x, mouseScreenPosition.x, 1), Mathf.Lerp(puckRB.position.y, mouseScreenPosition.y, 1), 0);
        puckRB.MovePosition(ScreenPos);
    }

    private void Start()
    {
        puckRB = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isInHand) {
            FollowCursor();
        }
    }
}
