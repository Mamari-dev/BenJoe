using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] Player player;
    bool isAction;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(player.InputController.PressAction)
        {
            if(collision.gameObject.CompareTag("Door"))
            {
                collision.gameObject.GetComponent<IInteract>().Interact();
            }
        }
    }
}
