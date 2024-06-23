using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MainMenu = 0,
    Options,
    Credits,
    LoadingScreen,
    HUD,
    PauseMenu,
    Game,
    GameEnemies,
    Nodes
}


public enum CursorTypes
{
    UI = 0,
    None
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public delegate void CollectMemoryPartEvent(PairID _pair, PanoramaPart _part);
    public event CollectMemoryPartEvent CollectMemoryPart;

    public delegate void CollectMemoryPairEvent(bool _collected);
    public event CollectMemoryPairEvent CollectMemoryPair;

    public delegate void OpenMemoryEvent();
    public event OpenMemoryEvent OpenMemory;

    public delegate void OpenEndscreenEvent();
    public event OpenEndscreenEvent OpenEndscreen;

    private void Awake()
    {
        // Destroy Instance when it is already existing, else create it
        if (Instance == null)
            Instance = (UIManager)this;
        else if (Instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    #region SceneManagement

    /// <summary>
    /// Load new Scene
    /// </summary>
    /// <param name="_scene">Which Scene</param>
    /// <param name="_loadMode">Single or Additive</param>
    /// <param name="_cursorType">Which Cursor</param>
    public void LoadSceneAsync(Scenes _scene, LoadSceneMode _loadMode = LoadSceneMode.Single, CursorTypes _cursorType = CursorTypes.UI)
    {
        SetCursorType(_cursorType);
        SceneManager.LoadSceneAsync((int)_scene, _loadMode);
    }

    public void UnloadSceneAsync(Scenes _scene, CursorTypes _cursorType = CursorTypes.UI)
    {
        SceneManager.UnloadSceneAsync((int)_scene);
        SetCursorType(_cursorType);
    }

    public void SetCursorType(CursorTypes _cursorType)
    {
        switch (_cursorType)
        {
            case CursorTypes.UI:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case CursorTypes.None:
                Cursor.visible = false;
                break;
        }
    }

    #endregion

    public void CollectMemoryPartUI(PairID _pair, PanoramaPart _part)
    {
        Debug.Log("Collect");
        CollectMemoryPart.Invoke(_pair, _part);
    }

    /// <summary>
    /// true = the second MemoryPart is collected || false = the first MemoryPart is Lost
    /// </summary>
    /// <param name="_collected"></param>
    public void CollectMemoryPairUI(bool _collected)
    {
        CollectMemoryPair.Invoke(_collected);
    }

    public void OpenMemoryUI()
    {
        Debug.Log("Open pls");
        OpenMemory.Invoke();
    }

    public void Endscreen()
    {
        OpenEndscreen.Invoke();
    }

    private void OnDisable()
    {
        CollectMemoryPart = null;
        CollectMemoryPair = null;
        OpenMemory = null;
        OpenEndscreen = null;
    }

    #region Input Handling

    public void OnEscape(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            // close
            if (SceneManager.GetSceneByBuildIndex((int)Scenes.Options).isLoaded) UnloadSceneAsync(Scenes.Options);
            else if (SceneManager.GetSceneByBuildIndex((int)Scenes.PauseMenu).isLoaded) UnloadSceneAsync(Scenes.PauseMenu, CursorTypes.None);
            
            // open
            else if (SceneManager.GetSceneByBuildIndex((int)Scenes.MainMenu).isLoaded) LoadSceneAsync(Scenes.Options, LoadSceneMode.Additive);
            else if (SceneManager.GetSceneByBuildIndex((int)Scenes.HUD).isLoaded) LoadSceneAsync(Scenes.PauseMenu, LoadSceneMode.Additive);
        }
    }

    #endregion
}
