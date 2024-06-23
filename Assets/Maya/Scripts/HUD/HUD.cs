using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Memory")]
    [SerializeField] private GameObject panorama;
    [SerializeField] private float panoramaShowTimer = 2f;
    [SerializeField] private MemoryParts[] memories;
    private MemoryParts currentMemory;

    // Update is called once per frame
    void Update()
    {
        timer.text = GameManager.Instance.Timer.ToString("00");
    }

    private void OnEnable()
    {
        panorama.SetActive(false);

        UIManager.Instance.CollectMemoryPart += CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair += CollectMemoryPair;
        UIManager.Instance.OpenMemory += OpenMemory;
    }

    private void OnDisable()
    {
        UIManager.Instance.CollectMemoryPart -= CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair -= CollectMemoryPair;
        UIManager.Instance.OpenMemory -= OpenMemory;
    }

    private void OpenMemory()
    {
        StartCoroutine(OpenMemoryShort());
    }

    private IEnumerator OpenMemoryShort()
    {
        panorama.SetActive(true);
        yield return new WaitForSeconds(panoramaShowTimer);
        panorama.SetActive(false);
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
