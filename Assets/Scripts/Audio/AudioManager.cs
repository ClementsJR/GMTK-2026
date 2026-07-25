using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_asNarrationSource;

    [SerializeField] private AudioSource m_asEffectSource;

    [SerializeField] private AudioSource m_asMusicSource;

    public bool m_bIsNarrationPlaying => m_asNarrationSource.isPlaying;
    public bool m_bIsEffectPlaying => m_asEffectSource.isPlaying;
    public bool m_bIsMusicPlaying => m_asMusicSource.isPlaying;

    public AudioClip m_acCurrentNarrationClip => m_asNarrationSource.clip;
    public AudioClip m_acCurrentEffectClip => m_asEffectSource.clip;
    public AudioClip m_acCurrentMusicClip => m_asMusicSource.clip;

    // Events
    public event Action<AudioClip> m_eOnNarrationClipStarted;
    public event Action<AudioClip> m_eOnNarrationClipStopped;

    public event Action<AudioClip> m_eOnEffectClipStarted;
    public event Action<AudioClip> m_eOnEffectClipStopped;

    public event Action<AudioClip> m_eOnMusicClipStarted;
    public event Action<AudioClip> m_eOnMusicClipStopped;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DebugManager.Log(this, "Awake Finished");
    }

    private void Update()
    {
        // Detect natural clip end
        if (m_asNarrationSource.clip != null &&
            !m_asNarrationSource.isPlaying &&
            m_asNarrationSource.time > 0f)
        {
            AudioClip finishedClip = m_asNarrationSource.clip;
            m_asNarrationSource.clip = null;
            m_asNarrationSource.time = 0f;

            m_eOnNarrationClipStopped?.Invoke(finishedClip);
        }

        // Detect natural clip end
        if (m_asEffectSource.clip != null &&
            !m_asEffectSource.isPlaying &&
            m_asEffectSource.time > 0f)
        {
            AudioClip finishedClip = m_asEffectSource.clip;
            m_asEffectSource.clip = null;
            m_asEffectSource.time = 0f;

            m_eOnEffectClipStopped?.Invoke(finishedClip);
        }

        // Detect natural clip end
        if (m_asMusicSource.clip != null &&
            !m_asMusicSource.isPlaying &&
            m_asMusicSource.time > 0f)
        {
            AudioClip finishedClip = m_asMusicSource.clip;
            m_asMusicSource.clip = null;
            m_asMusicSource.time = 0f;

            m_eOnMusicClipStopped?.Invoke(finishedClip);
        }
    }

    public void PlayNarrationClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopNarrationClip();

        m_asNarrationSource.clip = clip;
        m_asNarrationSource.Play();

        Debug.Log("OnNarrationClipStartedInvoke");
        m_eOnNarrationClipStarted?.Invoke(clip);
    }

    public void PlayEffectClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopEffectClip();

        m_asEffectSource.clip = clip;
        m_asEffectSource.Play();

        m_eOnEffectClipStarted?.Invoke(clip);
    }

    public void PlayMusicClip(AudioClip clip)
    {
        if (clip == null)
            return;

        StopMusicClip();

        m_asMusicSource.clip = clip;
        m_asMusicSource.Play();

        m_eOnMusicClipStarted?.Invoke(clip);
    }

    public void StopNarrationClip()
    {
        if (!m_asNarrationSource.isPlaying)
            return;

        AudioClip stoppedClip = m_asNarrationSource.clip;

        m_asNarrationSource.Stop();
        m_asNarrationSource.clip = null;
        m_asNarrationSource.time = 0f;

        m_eOnNarrationClipStopped?.Invoke(stoppedClip);
    }

    public void StopEffectClip()
    {
        if (!m_asEffectSource.isPlaying)
            return;

        AudioClip stoppedClip = m_asEffectSource.clip;

        m_asEffectSource.Stop();
        m_asEffectSource.clip = null;
        m_asEffectSource.time = 0f;

        m_eOnEffectClipStopped?.Invoke(stoppedClip);
    }

    public void StopMusicClip()
    {
        if (!m_asMusicSource.isPlaying)
            return;

        AudioClip stoppedClip = m_asMusicSource.clip;

        m_asMusicSource.Stop();
        m_asMusicSource.clip = null;
        m_asMusicSource.time = 0f;

        m_eOnMusicClipStopped?.Invoke(stoppedClip);
    }

    public void StopAllClips()
    {
        StopNarrationClip();
        StopEffectClip();
        StopMusicClip();
    }
}
