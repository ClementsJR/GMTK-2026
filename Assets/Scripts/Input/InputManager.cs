using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static IGameInput Current { get; private set; }

    [SerializeField] private UnityGameInput unityInput;

    void Awake()
    {
        Current = unityInput;
    }


}