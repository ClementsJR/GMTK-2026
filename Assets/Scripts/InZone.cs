using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InZone : MonoBehaviour
{
    public GameObject target;
    private BoxCollider2D zone;
    public UnityEvent targetInZone;
    void Start()
    {
        if(zone == null)
        {
            zone = this.GetComponent<BoxCollider2D>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            Debug.Log("Target entered goal zone, invoking end event");//TODO remove when done
            targetInZone.Invoke();
        }
    }
}
