using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_teleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    public GameObject E;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                currentTeleporter.GetComponent<teleporters>().SwitchConfiner();
                transform.position = currentTeleporter.GetComponent<teleporters>().GetDestination().position;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                E.SetActive(false);
            }
        }
    }
}
