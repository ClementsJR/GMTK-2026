using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void StartInteract(GameObject interactor);
    void EndInteract(GameObject interactor);
}
