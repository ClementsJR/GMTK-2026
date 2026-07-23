using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

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

        DebugManager.Log(this, "Start Finished");

    }

    //Load Scene Immediately
    public void LoadScene(string p_sSceneName)
    {
        AudioManager.Instance.StopAllClips();
        SceneManager.LoadScene(p_sSceneName);
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
        yield return null;

        yield return InitSceneIfAvailable();
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

        // Wait one frame so the new scene initializes
        yield return null;


        yield return InitSceneIfAvailable();

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


        yield return InitSceneIfAvailable();



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


}
