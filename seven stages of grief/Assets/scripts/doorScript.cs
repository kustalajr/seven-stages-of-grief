using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public bool locked;
    public Animator anim;
    void Start()
    {
        locked = true;
    }

    void Update()
    {
        if(locked == false)
        {
            anim.SetBool("OpenDaFreakingHatch", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Key"))
        {
            locked = false;
            Destroy(other.gameObject);
        }
    }
}
