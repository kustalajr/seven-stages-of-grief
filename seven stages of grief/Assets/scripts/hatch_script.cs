using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatch_script : MonoBehaviour, IInteractable
{
    public bool isOpened { get; private set; }
    public string HatchID { get; private set; }
    [SerializeField] private Animator anim;

    void Start()
    {
        HatchID ??= Global_Helper.GenerateUniqueID(gameObject);

    }

    public bool canInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!canInteract()) return;
        OpenHatch();
    }

    private void OpenHatch()
    {
        anim.SetBool("OpenDaFreakingHatch", true);
    }
}
