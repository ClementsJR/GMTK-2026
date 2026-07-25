using UnityEngine;
using System.Collections;

public class MG1Controller : MonoBehaviour, ISceneInitializable
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndMinigame()
    {

        SceneTransitionManager.Instance.EndMinigame(
            new MinigameResult
            {
                Success = true
            });

    }

    public IEnumerator Initialize()
    {
        yield return null;

        yield return FadeManager.Instance.FadeInCoroutine();
    }

    //public void PauseGameplay()
    //{

    //    if (_isPaused)
    //        return;

    //    _isPaused = true;

    //    foreach (GameObject root in gameplayRoots)
    //    {
    //        if (root != null)
    //            root.SetActive(false);
    //    }

    //}

    //public void ResumeGameplay()
    //{
    //    if (!_isPaused)
    //        return;

    //    _isPaused = false;

    //    foreach (GameObject root in gameplayRoots)
    //    {
    //        if (root != null)
    //            root.SetActive(true);
    //    }

    //}
}
