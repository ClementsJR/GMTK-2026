using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneInitializable
{
    IEnumerator Initialize();
}

public interface IPausableGameplay
{
    void PauseGameplay();
    void ResumeGameplay();
}
