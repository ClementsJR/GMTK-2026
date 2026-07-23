using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapController : MonoBehaviour
{

    [SerializeField] private float m_fBootStrapDelaySeconds = 0.0f;

    [SerializeField] private string m_sNextSceneName = "SplashScreensScene";

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(BootStrapComplete());

        DebugManager.Log(this, "Start Finished");
    }


    IEnumerator BootStrapComplete()
    {
        yield return new WaitForSeconds(m_fBootStrapDelaySeconds);

        SceneTransitionManager.Instance.FadeLoadingScene(m_sNextSceneName, true);

    }
}
