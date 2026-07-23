using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BobberInteraction : MonoBehaviour
{
    public bool isHeld = false;
    public int decrementValue;
    public int incrementValue;
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
        bobber.value += incrementValue;
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
            bobber.value -= decrementValue;
        }

        if(bobber.value < 0)
        {
            bobber.value = 0;
        }
        else if(bobber.value > bobber.maxValue)
        {
            bobber.value = bobber.maxValue;
        }
    }
}
