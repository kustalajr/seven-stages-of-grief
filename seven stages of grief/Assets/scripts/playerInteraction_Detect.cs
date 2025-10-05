using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerInteraction_Detect : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject E;
    void Start()
    {
        E.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableInRange != null)
            {
                interactableInRange.Interact();
                E.SetActive(false);
            }
            else
            {
                print("No interactable in range");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.canInteract()) 
        {
            interactableInRange = interactable;
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            E.SetActive(false);
        }
    }
}
