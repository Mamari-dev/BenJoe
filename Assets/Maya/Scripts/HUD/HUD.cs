using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Memory")]
    [SerializeField] private CanvasGroup panoramaCanvas;
    [SerializeField] private float panoramaShowTimer = 2f;
    [SerializeField] private float panoramaFadeTimer = 2f;
    [SerializeField] private float panoramaAlpha = 1f;
    [SerializeField] private MemoryParts[] memories;
    private MemoryParts currentMemory;

    [Header("Endscreen")]
    [SerializeField] private CanvasGroup endscreen;
    [SerializeField] private float endscreenShowTimer = 8f;
    [SerializeField] private float endscreenFadeTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        timer.text = GameManager.Instance.Timer.ToString("00");
    }

    private void OnEnable()
    {
        panoramaCanvas.alpha = 0f;
        endscreen.gameObject.SetActive(false);
        endscreen.alpha = 0f;

        UIManager.Instance.CollectMemoryPart += CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair += CollectMemoryPair;
        UIManager.Instance.OpenMemory += OpenMemory;
        UIManager.Instance.OpenEndscreen += OpenEndscreen;
    }

    private void OnDisable()
    {
        UIManager.Instance.CollectMemoryPart -= CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair -= CollectMemoryPair;
        UIManager.Instance.OpenMemory -= OpenMemory;
        UIManager.Instance.OpenEndscreen -= OpenEndscreen;
    }

    private void OpenEndscreen()
    {
        endscreen.gameObject.SetActive(true);
        LeanTween.alphaCanvas(panoramaCanvas, panoramaAlpha, endscreenFadeTimer);
        UIManager.Instance.SetCursorType(CursorTypes.UI);
    }

    public void OnMainMenu()
    {
        UIManager.Instance.LoadSceneAsync(Scenes.MainMenu);
    }

    private void OpenMemory()
    {
        StartCoroutine(OpenMemoryShort());
    }

    private IEnumerator OpenMemoryShort()
    {
        LeanTween.alphaCanvas(panoramaCanvas, panoramaAlpha, panoramaFadeTimer);
        yield return new WaitForSeconds(panoramaShowTimer);
        LeanTween.alphaCanvas(panoramaCanvas, 0f, panoramaFadeTimer);
    }


    private void CollectMemoryPart(PairID _pair, PanoramaPart _part)
    {
        if ((currentMemory = GetMemory(_pair, _part)) != null)
        {
            currentMemory.ActivateMemory();
        }

        OpenMemory();
    }
    private void CollectMemoryPair(bool _collected)
    {
        if (_collected)
        {
            PanoramaPart part;
            if (currentMemory.Part == PanoramaPart.Top) part = PanoramaPart.Bottom;
            else part = PanoramaPart.Top;

            GetMemory(currentMemory.Pair, part).ActivateMemory(); 
        }
        else
        {
            currentMemory.DeactivateMemory();
        }

        currentMemory = null;

        OpenMemory();
    }

    private MemoryParts GetMemory(PairID _pair, PanoramaPart _part)
    {
        for (int i = 0; i < memories.Length; i++)
        {
            if (memories[i].Pair == _pair)
                if (memories[i].Part == _part)
                    return memories[i];
        }

        return null;
    }
}
