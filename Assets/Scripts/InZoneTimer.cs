using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InZoneTimer : MonoBehaviour
{
    public float timeDuration;
    public GameObject target;
    private BoxCollider2D zone;
    private bool isTimerRunning = false;
    public UnityEvent timerEnd;
    private float currTimer;
    void Start()
    {
        float currTimer = timeDuration;
        if(zone == null)
        {
            zone = this.GetComponent<BoxCollider2D>();
        }
        ZonePlacement();
    }

    private void ZonePlacement()
    {
        float goalPos = UnityEngine.Random.Range(-40, 78);
        //Current top position is Y 78 top and Y -40 bottom not best solution, but works
        this.transform.localPosition = new Vector2(0, goalPos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            isTimerRunning = true;
            currTimer = timeDuration;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isTimerRunning = false;
        }
    }

    void Update()
    {
        if (!isTimerRunning)
        {
            return;
        }
        else
        {
            if (currTimer > 0)
            {
                currTimer -= Time.deltaTime;
            }
            else if(currTimer == 0 || currTimer < 0)
            {
                isTimerRunning = false;
                Debug.Log("Timer ran out, invoking timer end event");//TODO remove when done
                timerEnd.Invoke();
            }
        }
    }
}
