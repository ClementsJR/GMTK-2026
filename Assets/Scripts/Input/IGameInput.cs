using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameInput
{
    Vector2 Move { get; }
    Vector2 MouseScreenPosition { get; }

    bool JumpPressed { get; }
    bool JumpHeld { get; }

    bool InteractPressed { get; }
    bool InteractHeld { get; }

    bool CancelPressed { get; }
    bool CancelHeld { get; }
}
