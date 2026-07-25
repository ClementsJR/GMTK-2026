using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessSpawner : MonoBehaviour
{
    public GameObject messPrefab;
    [SerializeField] private int numOfMessesToSpawn;
    public UnityEvent onMessCleaned;

    public void checkRemainingMess()
    {
        if (this.transform.childCount-1 > 0)
        {
            Debug.Log($"Not all messes are cleaned. {transform.childCount-1} remain");//TODO remove when done
            return;
        }
        else
        {
            Debug.Log("All messes cleaned");//TODO remove when done
            onMessCleaned.Invoke();
        }
    }
    void Start()
    {
        for(int i = 0; i < numOfMessesToSpawn; i++)
        {
            GameObject currMess = Instantiate(messPrefab, this.transform);
            currMess.GetComponent<messInteraction>().onCleanComplete.AddListener(checkRemainingMess);
        }
    }
}
