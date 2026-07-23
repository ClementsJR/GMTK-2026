using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistedSystems : MonoBehaviour
{

    public static PersistedSystems Instance;

    [SerializeField]
    private List<GameObject> persistedObjects = new List<GameObject>();


    private void Awake()
    {
        // Singleton pattern (jam-safe version)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (GameObject obj in persistedObjects)
        {

            GameObject newobj = Instantiate(obj);
            newobj.transform.parent = transform;

        }


        DontDestroyOnLoad(gameObject);

        DebugManager.Log(this, "Awake Finished");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
