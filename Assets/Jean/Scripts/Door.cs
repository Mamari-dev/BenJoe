using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private PairID pairID;
    [SerializeField] private PanoramaPart panoramaPart;

    public void Interact()
    {
        GameManager.Instance.TestPairID(pairID, panoramaPart);
    }

    public void OpenDoor()
    {

    }


}
