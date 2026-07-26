using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessSpawner : MonoBehaviour
{
    public GameObject messPrefab;
    [SerializeField] private int numOfMessesToSpawn;
    public UnityEvent onMessCleaned;
    private float Width = Screen.width;
    private float Height = Screen.height;

    public void checkRemainingMess()
    {
        if (this.transform.childCount-1 > 0)
        {
            return;
        }
        else
        {
            onMessCleaned.Invoke();
        }
    }

    private void randomizeLocations(GameObject objToMove)
    {
        float randomX = Random.Range(-Width / 2f, Width / 2f);
        float randomY = Random.Range(-Height / 2f, Height / 2f);

        objToMove.transform.localPosition = new Vector3(randomX, randomY);
    }
    void Start()
    {
        for(int i = 0; i < numOfMessesToSpawn; i++)
        {
            GameObject currMess = Instantiate(messPrefab, this.transform);
            currMess.GetComponent<messInteraction>().onCleanComplete.AddListener(checkRemainingMess);
            randomizeLocations(currMess);
        }
    }
}
