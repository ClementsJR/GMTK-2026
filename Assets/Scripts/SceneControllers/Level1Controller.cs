using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour, ISceneInitializable
{

    [SerializeField] private string m_sNextSceneName = "hiJohn -- uWu";




    //IEnumerator NewGameStart()
    //{

    //    DebugManager.Log(this, "Starting new game in scene -- " + m_sNextSceneName);
    //    SceneTransitionManager.Instance.FadeLoadingScene(m_sNextSceneName, true);

    //    yield return null;
    //}


    public IEnumerator Initialize()
    {
        yield return null;

        yield return FadeManager.Instance.FadeInCoroutine();
    }

}
