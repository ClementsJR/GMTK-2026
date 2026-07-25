using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;



    [SerializeField] private Image m_imgFadeImage;
    [SerializeField] private float m_fFadeDuration = 1f;

    private Coroutine m_corCurrentFade;

    private void Awake()
    {
        // Singleton pattern (jam-safe version)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Ensure we start visible (black)
        SetAlpha(1f);
    }


    public void FadeOut()
    {
        DebugManager.Log(this, "Fade Out");
        StartFade(1f);
    }

    public void FadeIn()
    {
        DebugManager.Log(this, "Fade In");
        StartFade(0f);
    }

    public IEnumerator FadeOutCoroutine()
    {
        DebugManager.Log(this, "Fade Out Coroutine");
        yield return StartFadeRoutine(1f);
    }

    public IEnumerator FadeInCoroutine()
    {
        DebugManager.Log(this, "Fade In Coroutine");
        yield return StartFadeRoutine(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (m_corCurrentFade != null)
            StopCoroutine(m_corCurrentFade);

        m_corCurrentFade = StartCoroutine(StartFadeRoutine(targetAlpha));
    }

    private IEnumerator StartFadeRoutine(float targetAlpha)
    {
        if(m_imgFadeImage.color.a == targetAlpha)
        {
            DebugManager.Log(this, "Target fade already met in StartFadeRoutine -- Exiting");
            yield break;

        }

        float startAlpha = m_imgFadeImage.color.a;
        float time = 0f;

        while (time < m_fFadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / m_fFadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
        DebugManager.Log(this, "StartFadeRoutine completed");
    }

    private void SetAlpha(float alpha)
    {
        Color color = m_imgFadeImage.color;
        color.a = alpha;
        m_imgFadeImage.color = color;
    }
}
