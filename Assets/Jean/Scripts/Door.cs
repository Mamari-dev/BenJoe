using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private PanoramaSO panoramaSO;
    [SerializeField] private PairID pairID;
    [SerializeField] private PanoramaPart panoramaPart;

    [SerializeField] private CircleCollider2D interactCollider;
    [SerializeField] private PolygonCollider2D boxCollider;
    [SerializeField] private SpriteRenderer missingPart;
    [SerializeField] private SpriteRenderer panoSprite;
    [SerializeField] private float fadeSpeed, moveInSpeed;

    private bool interactable = true;

    private bool isOpened = false, fadeInPano = false, fadeOutPano = false;

    private float initialYpos;

    private void Update()
    {
        if (fadeInPano)
        {
            FadeInPano();
        }
        else if (fadeOutPano)
        {
            FadeOutPano();  
        }

        if(isOpened)
        {
            FadeInMissingPart();
        }
    }


    private void Awake()
    {
        foreach(PanoramaStruct panoramaStruct in panoramaSO.panoramaStructs)
        {
            if(panoramaStruct.pairID == pairID && panoramaStruct.part == panoramaPart)
            {
                panoSprite.sprite = panoramaStruct.panoramaPartSprite;
                break;
            }
        }

        initialYpos = missingPart.gameObject.transform.position.y;
        missingPart.color = new Color(missingPart.color.r, missingPart.color.g, missingPart.color.b, 0);
        missingPart.gameObject.transform.position += new Vector3(0,2,0);
    }

    public void Interact()
    {
        if (interactable)
        {
            interactable = GameManager.Instance.TestPairID(pairID, panoramaPart);
        }
        if (!interactable)
        {
            fadeOutPano = true;
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        SetAllFadesFalse();
        interactable = isInteractable;
        if (isInteractable)
        {
            fadeInPano = true;
        }
        else 
        { 
            fadeOutPano = false;
        }
    }

    public void OpenDoor()
    {
        interactable = false;

        fadeOutPano = true;

        isOpened = true;
    }

    private void FadeOutPano()
    {
        if(panoSprite.color.a > 0)
        {
            panoSprite.color -= new Color(0, 0, 0, fadeSpeed) * Time.deltaTime;
        }
        else
        {
            SetAllFadesFalse();
        }
    }

    private void FadeInPano()
    {
        if (panoSprite.color.a < 1)
        {
            panoSprite.color += new Color(0, 0, 0, fadeSpeed) * Time.deltaTime;
        }
        else
        {
            SetAllFadesFalse();
        }
    }

    private void FadeInMissingPart()
    {
        if(missingPart.color.a < 1)
        {
            missingPart.color += new Color(0, 0, 0, fadeSpeed) * Time.deltaTime;
            
        }
        else
        {
            SetAllFadesFalse();
        }

        if(missingPart.gameObject.transform.position.y >= initialYpos)
        {
            missingPart.gameObject.transform.position -= new Vector3(0, moveInSpeed, 0) * Time.deltaTime;
        }
        else
        {
            missingPart.gameObject.transform.position = new Vector3(missingPart.gameObject.transform.position.x, 
                initialYpos, 
                missingPart.gameObject.transform.position.z);

            boxCollider.enabled = false;
            isOpened = false;
        }
        
    }


    private void SetAllFadesFalse()
    {
        fadeInPano = false;
        fadeOutPano = false;
    }
}
