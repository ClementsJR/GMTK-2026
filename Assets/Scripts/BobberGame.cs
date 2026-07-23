using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BobberGame : MonoBehaviour
{
    public bool isHeld = false;
    [SerializeField] private Slider bobber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(bobber == null)
        {
            GameObject.Find("Bobber").GetComponent<Slider>();
        }
    }

    public void toggleHold(bool state)
    {
        isHeld = state;
    }
    public void raiseBobber()
    {
        bobber.value += 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHeld)
        {
            raiseBobber();
        }
        if(bobber.value != 0 && bobber.value > 0)
        {
            bobber.value -= 1;
        }
    }
}
