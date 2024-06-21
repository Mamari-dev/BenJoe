using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Debug.Log("1f");
    }

    public void OnResume()
    {
        UIManager.Instance.UnloadSceneAsync(Scenes.PauseMenu, CursorTypes.None);
    }

    public void OnOptions()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.Options, LoadSceneMode.Additive, CursorTypes.UI);
    }

    public void OnExit()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.MainMenu);
    }
}
