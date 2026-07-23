using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, ISceneInitializable
{

    [SerializeField] private string m_sNextSceneName = "TestCaveScene";

    public void OnPlayPressed()
    {
        DebugManager.Log(this, "Play Pressed");
        StartCoroutine(NewGameStart());
    }

    public void OnLoadPressed()
    {
        DebugManager.Log(this, "Load Pressed");
        // open submenu
    }

    public void OnSettingsPressed()
    {
        DebugManager.Log(this, "Settings Pressed");

    }

    public void OnQuitPressed()
    {
        DebugManager.Log(this, "Quit Pressed");
        Application.Quit();
    }





    IEnumerator NewGameStart()
    {

        DebugManager.Log(this, "Starting new game in scene -- " + m_sNextSceneName);
        SceneTransitionManager.Instance.FadeLoadingScene(m_sNextSceneName, true);

        yield return null;
    }


    public IEnumerator Initialize()
    {
        yield return null;

        yield return FadeManager.Instance.FadeInCoroutine();
    }

}
