using UnityEngine;
using System.Collections;

public class SampleSceneController : MonoBehaviour, ISceneInitializable, IPausableGameplay
{

    [SerializeField] private GameObject[] gameplayRoots;

    [SerializeField] private string MinigameToLoad;

    private bool _isPaused;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Current.JumpPressed)
        {

            SceneTransitionManager.Instance.LoadMinigame(MinigameToLoad);

        }
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
}
