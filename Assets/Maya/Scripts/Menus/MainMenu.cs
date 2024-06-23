using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        AnimateButtonsIn();
    }

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
    /// Load Credits Menu Additive
    /// </summary>
    public void OnCredits()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.Credits, LoadSceneMode.Additive, CursorTypes.UI);
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

    #region Animations

    [SerializeField] private GameObject[] buttons;
    [SerializeField] private float animTime;

    private void AnimateButtonsIn()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i % 2 == 0)
                AnimateOneButtonIn(i, true);    // animate one button from the right side
            else
                AnimateOneButtonIn(i, false);   // and the next one from the left side
        }
    }

    private void AnimateOneButtonIn(int _idx, bool _right)
    {
        Transform endTransf = buttons[_idx].transform;

        //buttons[_idx].transform.position.x =  // canvas width
        LeanTween.moveX(buttons[_idx], endTransf.position.x, animTime);
    }

    private void AnimateButtonsOut()
    {

    }

    #endregion
}
