using UnityEngine;

public class MG3ZoneMixer : MonoBehaviour
{
    [SerializeField] private GameObject[] zones;
    private int numOfZones;

    private void Start()
    {
        numOfZones = zones.Length;
        for(int i = 0; i < zones.Length; i++)
        {
            if (zones[i].activeInHierarchy == true)
            {
                zones[i].SetActive(false);
            }
        }
        int randomChoice = UnityEngine.Random.Range(0, numOfZones);
        zones[randomChoice].SetActive(true);
    }
}
