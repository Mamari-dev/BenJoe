using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private PairID pairID;
    [SerializeField] private PanoramaPart panoramaPart;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Sprite missingPart;
    [SerializeField] private Sprite panoSprite;

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
