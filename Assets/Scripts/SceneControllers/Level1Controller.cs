using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Level1Controller : MonoBehaviour, ISceneInitializable, IPausableGameplay
{

    [SerializeField] private GameObject[] gameplayRoots;

    [SerializeField] private List<string> MinigameList = new();


    private bool _isPaused;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchMinigame(int minigameNumber)
    {


        SceneTransitionManager.Instance.LoadMinigame(MinigameList[minigameNumber], OnMinigameFinished);


    }

    public IEnumerator Initialize()
    {
        yield return null;

        yield return FadeManager.Instance.FadeInCoroutine();
    }

    public void PauseGameplay()
    {

        if (_isPaused)
            return;

        _isPaused = true;

        foreach (GameObject root in gameplayRoots)
        {
            if (root != null)
                root.SetActive(false);
        }

    }

    public void ResumeGameplay()
    {

        Debug.Log("UNPAUSING");

        if (!_isPaused)
            return;

        _isPaused = false;

        foreach (GameObject root in gameplayRoots)
        {
            if (root != null)
                root.SetActive(true);
        }

    }

    private void OnMinigameFinished(MinigameResult result)
    {
        if (result.Success)
        {
            Debug.Log("Player won!");
        }
        else
        {
            Debug.Log("Player lost!");
        }
    }

}
