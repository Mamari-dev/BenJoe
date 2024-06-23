using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryParts : MonoBehaviour
{
    [SerializeField] private GameObject picture;
    [Space]
    [SerializeField] private PairID pair;
    [SerializeField] private PanoramaPart part;

    private void OnEnable()
    {
        picture = GetComponentInChildren<Transform>().gameObject;
        picture.SetActive(false);
    }

    public void ActivateMemory()
    {
        picture.SetActive(true);
    }

    public void DeactivateMemory()
    {
        picture.SetActive(false);
    }

    public PairID Pair { get => pair; }
    public PanoramaPart Part { get => part; }
}
