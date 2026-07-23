using System;
using System.Collections.Generic;
using UnityEngine;

public static class DebugManager
{
    public static bool EnableUnityLogs = true;
    public static bool EnableFileLogging = true;

    private static readonly List<string> _buffer = new List<string>(1024);
    private static readonly object _lock = new object();

    private static string _logFilePath;
    private static float _lastFlushTime;
    private static float _flushInterval = 2f;

    private static bool _initialized = false;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void RuntimeInit()
    {
        Initialize();
    }

    // -------------------------
    public static void Initialize()
    {
        if (_initialized) return;

        if (!EnableFileLogging)
        {
            _initialized = true;
            return;
        }

        if (PersistedDataManager.Instance == null)
        {
            return;
        }

        _logFilePath = PersistedDataManager.Instance.CreateLogFile();

        _initialized = true;

    }

    // -------------------------
    public static void Log(object caller, string message)
    {
        Write("LOG", caller, message);

        if (EnableUnityLogs)
            Debug.Log($"{caller}: {message}");
    }

    public static void Warning(object caller, string message)
    {
        Write("WARN", caller, message);

        if (EnableUnityLogs)
            Debug.LogWarning($"{caller}: {message}");
    }

    public static void Error(object caller, string message)
    {
        Write("ERROR", caller, message);

        if (EnableUnityLogs)
            Debug.LogError($"{caller}: {message}");
    }

    // -------------------------
    private static void Write(string level, object caller, string message)
    {
        if (!EnableFileLogging) return;

        if (!_initialized)
        {
            Initialize();

            if (!_initialized)
                return;
        }

        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        string line = $"[{timestamp}] [{level}] {caller}: {message}";

        lock (_lock)
        {
            _buffer.Add(line);
        }
    }

    // -------------------------
    public static void Update()
    {
        if (!EnableFileLogging || !_initialized) return;

        if (Time.unscaledTime - _lastFlushTime > _flushInterval)
        {
            Flush();
            _lastFlushTime = Time.unscaledTime;
        }
    }

    public static void Flush()
    {
        if (!EnableFileLogging || !_initialized) return;
        if (_buffer.Count == 0) return;

        List<string> copy;

        lock (_lock)
        {
            copy = new List<string>(_buffer);
            _buffer.Clear();
        }

        if (PersistedDataManager.Instance == null)
            return;

        PersistedDataManager.Instance.AppendLines(_logFilePath, copy.ToArray());
    }

    // -------------------------
    public static void Shutdown()
    {
        Flush();
    }
}