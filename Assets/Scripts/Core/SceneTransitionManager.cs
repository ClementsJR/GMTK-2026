using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;


public class MinigameResult
{
    public bool Success;
    // public int Score;
    // public float Time;
}

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public bool IsMinigameActive { get; private set; }

    private Scene _loadedMinigameScene;

    private System.Action<MinigameResult> _onMinigameFinished;

    private Scene _currentScene;

    private void Awake()
    {
        // Singleton pattern (jam-safe version)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DebugManager.Log(this, "Awake Finished");

    }

    private void Start()
    {

        // Fade in when the game first starts
        FadeManager.Instance.FadeIn();

        _currentScene = SceneManager.GetActiveScene();

        DebugManager.Log(this, "Start Finished");

    }

    //Load Scene Immediately
    public void LoadScene(string p_sSceneName)
    {
        AudioManager.Instance.StopAllClips();
        SceneManager.LoadScene(p_sSceneName);

        _currentScene = SceneManager.GetSceneByName(p_sSceneName);
    }



    //Load Scene Immediately after delay
    public void LoadSceneAfterDelay(string p_sSceneName, float p_fDelay)
    {
        StartCoroutine(LoadAfterDelay(p_sSceneName, p_fDelay));
    }

    private IEnumerator LoadAfterDelay(string p_sSceneName, float p_fDelay)
    {
        DebugManager.Log(this, "Changing scene with delay -- " + p_sSceneName + " -- " + p_fDelay);

        yield return new WaitForSeconds(p_fDelay);
        AudioManager.Instance.StopAllClips();
        SceneManager.LoadScene(p_sSceneName);

        _currentScene = SceneManager.GetSceneByName(p_sSceneName);
        yield return null;

        //yield return InitSceneIfAvailable();
        yield return InitScene(_currentScene);
    }



    //Load Scene immediately with preceeding fade out
    public void FadeScene(string p_sSceneName)
    {
        StartCoroutine(LoadSceneWithFade(p_sSceneName));
    }

    private IEnumerator LoadSceneWithFade(string p_sSceneName)
    {
        DebugManager.Log(this, "Changing scene with fade -- " + p_sSceneName);

        // Fade to black
        yield return FadeManager.Instance.FadeOutCoroutine();

        // Load new scene
        AudioManager.Instance.StopAllClips();

        SceneManager.LoadScene(p_sSceneName);

        _currentScene = SceneManager.GetSceneByName(p_sSceneName);

        // Wait one frame so the new scene initializes
        yield return null;


        //yield return InitSceneIfAvailable();
        yield return InitScene(_currentScene);

    }


    //Load Scene with loading bar and optional fade out
    public void FadeLoadingScene(string p_sSceneName, bool p_bShowLoadingBar)
    {

        StartCoroutine(FadeLoadSceneAsync(p_sSceneName, p_bShowLoadingBar));

    }

    private IEnumerator FadeLoadSceneAsync(string p_sSceneName, bool p_bShowLoadingBar)
    {

        DebugManager.Log(this, "Loading scene with fade -- " + p_sSceneName);

        //Fade out
        yield return FadeManager.Instance.FadeOutCoroutine();

        //Display Loading Bar
        if (p_bShowLoadingBar)
        {

            //TODO
            //Display Loading Bar

        }

        //End Audio
        AudioManager.Instance.StopAllClips();

        //Begin Loading Scene Prefabs
        AsyncOperation op = SceneManager.LoadSceneAsync(p_sSceneName);
        op.allowSceneActivation = false;

        //Wait for Load
        while (op.progress < 0.9f)
        {
            float progress = op.progress / 0.9f;

            if (p_bShowLoadingBar)
            {

                //Update Loading Bar

            }

            DebugManager.Log(this, p_sSceneName + " loading progress: " + progress);

            yield return null;
        }

        _currentScene = SceneManager.GetSceneByName(p_sSceneName);


        //Activate new loaded scene
        DebugManager.Log(this, p_sSceneName + " -- Activating Scene");
        op.allowSceneActivation = true;


        //Remove Loading Bar
        if (p_bShowLoadingBar)
        {

            //TODO
            //Remove Loading Bar

        }

        // Wait one frame so the new scene initializes - Awake, OnEnable, Start run here
        yield return null;


        //yield return InitSceneIfAvailable();
        yield return InitScene(_currentScene);



    }

    private IEnumerator InitSceneIfAvailable()
    {
        //Initialize the scene if it's controller implements ISceneInitializable
        var sceneControllers = FindObjectsByType(typeof(MonoBehaviour), FindObjectsSortMode.None).OfType<ISceneInitializable>();

        foreach (var controller in sceneControllers)
        {
            DebugManager.Log(this, "Calling Initialize on -- " + controller.ToString());
            yield return controller.Initialize();
        }
    }

    private IEnumerator InitScene(Scene scene)
    {
        
        while (!scene.isLoaded || scene.rootCount == 0)
        {
            DebugManager.Log(this, "Waiting to init " + scene.name);
            yield return null;
        }

        foreach (GameObject root in scene.GetRootGameObjects())
        {
            var controllers = root.GetComponentsInChildren<ISceneInitializable>(true);

            foreach (var controller in controllers)
            {
                yield return controller.Initialize();
            }
        }
    }


    public bool LoadMinigame(string sceneName, System.Action<MinigameResult> onFinished)
    {
        if (IsMinigameActive)
            return false;


        IsMinigameActive = true;

        _onMinigameFinished = onFinished;

        StartCoroutine(LoadMinigameRoutine(sceneName));

        return IsMinigameActive;
    }

    private IEnumerator LoadMinigameRoutine(string sceneName)
    {
        yield return FadeManager.Instance.FadeOutCoroutine();

        PauseGameplayScene();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
            yield return null;

        _loadedMinigameScene = SceneManager.GetSceneByName(sceneName);

        SceneManager.SetActiveScene(_loadedMinigameScene);

        yield return null;

        //yield return InitSceneIfAvailable();
        yield return InitScene(_loadedMinigameScene);

        yield return FadeManager.Instance.FadeInCoroutine();
    }

    private void PauseGameplayScene()
    {
        var systems = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPausableGameplay>();

        foreach (var system in systems)
            system.PauseGameplay();
    }

    private void ResumeGameplayScene()
    {
        Debug.Log("Unpausing");
        var systems = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IPausableGameplay>();

        foreach (var system in systems)
            system.ResumeGameplay();
    }

    public void EndMinigame(MinigameResult result)
    {
        StartCoroutine(UnloadMinigameRoutine(result));
    }

    private IEnumerator UnloadMinigameRoutine(MinigameResult result)
    {
        yield return FadeManager.Instance.FadeOutCoroutine();

        string sceneName = _loadedMinigameScene.name;

        AsyncOperation operation = SceneManager.UnloadSceneAsync(_loadedMinigameScene);

        while (!operation.isDone)
            yield return null;

        SceneManager.SetActiveScene(_currentScene);

        ResumeGameplayScene();

        IsMinigameActive = false;

        _onMinigameFinished?.Invoke(result);
        _onMinigameFinished = null;

        yield return FadeManager.Instance.FadeInCoroutine();
    }


}
