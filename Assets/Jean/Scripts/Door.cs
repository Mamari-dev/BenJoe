using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private PairID pairID;
    [SerializeField] private PanoramaPart panoramaPart;

    private bool interactable;

    public void Interact()
    {
        if (interactable)
        {
            interactable = GameManager.Instance.TestPairID(pairID, panoramaPart);
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        interactable = isInteractable;
    }

    public void OpenDoor()
    {
        interactable = false;
    }


}
