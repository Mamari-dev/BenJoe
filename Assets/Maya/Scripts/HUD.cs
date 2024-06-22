using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    float timer;

    // Update is called once per frame
    void Update()
    {
        // GameManager.Instance.Timer;
    }

    private void OnEnable()
    {
        UIManager.Instance.CollectMemoryPart += CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair += CollectMemoryPair;
    }

    private void OnDisable()
    {
        UIManager.Instance.CollectMemoryPart -= CollectMemoryPart;
        UIManager.Instance.CollectMemoryPair -= CollectMemoryPair;
    }

    private void CollectMemoryPart(PairID _pair, PanoramaPart _part)
    {

    }

    public void CollectMemoryPair(bool _collected)
    {

    }
}
