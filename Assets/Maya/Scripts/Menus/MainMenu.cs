using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region InputHandling

    /// <summary>
    /// Start Gameloop
    /// </summary>
    public void OnPlay()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.LoadingScreen, LoadSceneMode.Single, CursorTypes.None);
    }

    /// <summary>
    /// Load Options Menu Additive
    /// </summary>
    public void OnOptions()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.Options, LoadSceneMode.Additive, CursorTypes.UI);
    }

    /// <summary>
    /// Close the Game or Exit Playmode if in Unity
    /// </summary>
    public void OnExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    #endregion
}
