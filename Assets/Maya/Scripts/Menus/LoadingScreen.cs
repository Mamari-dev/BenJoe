using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public enum LoadingOffset
{
    Center,
    Left,
    Right
}

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Scenes sceneToLoad;
    [SerializeField] private CursorTypes cursorToLoad;

    [Header("Timings")]
    [SerializeField] private float loadDelay;
    [SerializeField] private float minLoadingTime;

    [Header("Bar and Image")]
    [SerializeField] private Slider loadingBar;
    [Space]
    [SerializeField] private GameObject icon;
    private Transform iconTransf;
    [Tooltip("Where the Icon starts moving")]
    [SerializeField] private Transform barLeft;
    [Tooltip("Where the Icon ends moving")]
    [SerializeField] private Transform barRight;
    [Tooltip("The Offset the icon has to the bar edge")]
    [SerializeField] private LoadingOffset iconOffset;
    private float iconXOffset;
    private bool moveIcon;

    private void OnEnable()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        loadingBar.value = 0f;
        if (icon != null)
        {
            iconTransf = icon.GetComponent<Transform>();

            switch (iconOffset)
            {
                case LoadingOffset.Left:
                    moveIcon = false;
                    iconXOffset = -(icon.GetComponent<RectTransform>().rect.width / 2);
                    iconTransf.position = new Vector2(barLeft.position.x - iconXOffset, iconTransf.position.y);
                    break;

                case LoadingOffset.Right:
                    moveIcon = true;
                    iconXOffset = icon.GetComponent<RectTransform>().rect.width / 2;
                    iconTransf.position = new Vector2(barLeft.position.x + iconXOffset, iconTransf.position.y);
                    break;

                default:
                    moveIcon = true;
                    iconXOffset = 0;
                    iconTransf.position = new Vector2(barLeft.position.x + iconXOffset, iconTransf.position.y);
                    break;
            }
        }

        yield return new WaitForSeconds(loadDelay);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)sceneToLoad);     // load scene 

        StartCoroutine(LoadingBar(loadOperation));
    }

    private IEnumerator LoadingBar(AsyncOperation _loadOperation)
    {
        _loadOperation.allowSceneActivation = false;     // Scene is not allowed to load (fake loadingBar)
        float iconPosition = 0f;
        var fakeBarTimer = 0f;

        while (_loadOperation.progress < 0.9f || fakeBarTimer <= minLoadingTime)
        {
            yield return null;
            fakeBarTimer += Time.unscaledDeltaTime;

            var waitProgress = fakeBarTimer / minLoadingTime;               // fake bar
            var loadingProgress = _loadOperation.progress / 0.9f;           // real loading bar
            loadingBar.value = Mathf.Min(loadingProgress, waitProgress);    // show the bar that is slower

            // enemy move with bar progress
            if (icon != null)
            {
                iconPosition = Mathf.Lerp(barLeft.position.x, barRight.position.x, loadingBar.value);
                iconPosition += iconXOffset;
                if (!moveIcon && iconXOffset < 0 && iconPosition + iconXOffset >= barLeft.position.x)
                {
                    moveIcon = true;
                }
                else if (moveIcon && iconXOffset > 0 && iconPosition + iconXOffset >= barRight.position.x)
                {
                    moveIcon = false;
                }

                if (moveIcon) iconTransf.position = new Vector2(iconPosition, iconTransf.position.y);
            }
        }

        if (!CheckForAdditiveScene())
            UIManager.Instance.SetCursorType(cursorToLoad);

        _loadOperation.allowSceneActivation = true;                              // load Scene

        yield return new WaitUntil(() => _loadOperation.isDone);
        loadingBar.value = 1f;

        StopAllCoroutines();
    }

    private bool CheckForAdditiveScene()
    {
        if (sceneToLoad == Scenes.Game)
        {
            if(SceneManager.sceneCount > (int)Scenes.GameEnemies)
                UIManager.Instance.LoadSceneAsync(Scenes.GameEnemies, LoadSceneMode.Additive, CursorTypes.None);
            UIManager.Instance.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive, CursorTypes.None);
            return true;
        }
        return false;
    }
}
