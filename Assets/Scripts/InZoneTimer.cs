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
                timerEnd.Invoke();
            }
        }
    }
}
