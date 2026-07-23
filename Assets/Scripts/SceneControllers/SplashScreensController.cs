using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SplashScreenEntry
{
    public Color backgroundColor;
    public Sprite image;
    public AudioClip audioClip;

    public float duration = 2f; // optional but very useful
}

public class SplashScreensController : MonoBehaviour, ISceneInitializable
{


    [SerializeField] private Image m_imgBackgroundImage;
    [SerializeField] private Image m_imgSplashImage;


    [SerializeField] private SplashScreenEntry[] m_arrSplashScreens;


    [SerializeField] private string m_sNextSceneName = "MainMenuScene";


    // Start is called before the first frame update
    void Start()
    {


        if(m_imgBackgroundImage == null)
        {

            GameObject.Find("SplashBackground").GetComponent<Image>();

        }

        if (m_imgSplashImage == null)
        {

            GameObject.Find("SplashImage").GetComponent<Image>();

        }

        if (m_arrSplashScreens.Length > 0 && m_arrSplashScreens[0] != null)
        {


        }
        else
        {
            DebugManager.Error(this, "Splash Screens Array is empty");
        }


        StartCoroutine(PlaySplashSequence());


        DebugManager.Log(this, "Start Finished");

    }



    IEnumerator PlaySplashSequence()
    {
        foreach (var entry in m_arrSplashScreens)
        {
            // Set background color
            if (m_imgBackgroundImage != null)
                m_imgBackgroundImage.color = entry.backgroundColor;

            // Set image
            if (m_imgSplashImage != null)
                m_imgSplashImage.sprite = entry.image;

            // Play audio
            if (AudioManager.Instance != null && entry.audioClip != null)
                AudioManager.Instance.PlayEffectClip(entry.audioClip);

            float timer = 0f;

            while (timer < entry.duration)
            {
                if (InputManager.Current.CancelHeld || InputManager.Current.JumpHeld)
                {
                    OnSequenceFinished();
                    yield break;
                }

                timer += Time.deltaTime;
                yield return null;
            }
        }

        OnSequenceFinished();
    }

    void OnSequenceFinished()
    {
        DebugManager.Log(this, "Splash Sequence done - Transitioning to next scene");
        SceneTransitionManager.Instance.FadeLoadingScene(m_sNextSceneName, true);
    }

    public IEnumerator Initialize()
    {
        yield return null;

        yield return FadeManager.Instance.FadeInCoroutine();

        DebugManager.Log(this, "Initialize Finished");
    }


}


