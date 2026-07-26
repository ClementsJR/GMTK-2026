using UnityEngine;
using UnityEngine.Events;

public class messInteraction : MonoBehaviour
{
    public UnityEvent onCleanComplete;
    private int numOfClicks;
    void Start()
    {
        numOfClicks = Random.Range(0, 5);
    }

    public void cleanMess()
    {
        if((numOfClicks-1) <= 0)
        {
            onCleanComplete.Invoke();
            Destroy(this.gameObject);
        }
        else
        {
            numOfClicks -= 1;
        }
    }
}
