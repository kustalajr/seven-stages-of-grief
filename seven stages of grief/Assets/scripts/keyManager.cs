using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool isPickedUp;
    private Vector2 vel;
    public float SmoothTime;
    void Start()
    {
        
    }

    void Update()
    {
        if(isPickedUp)
        {
            transform.position = Vector2.SmoothDamp(transform.position, Player.transform.position, ref vel, SmoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("New tag") && !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
