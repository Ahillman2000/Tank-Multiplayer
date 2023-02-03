using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickupable
{
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup(collision.GetComponent<Tank>());
        }
    }

    public void Pickup(Tank tank)
    {
        Debug.Log("Ammo pickup obtained");
        Destroy(this.gameObject);
    }

    void Update()
    {
        
    }
}
