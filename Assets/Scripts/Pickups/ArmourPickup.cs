using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickup : MonoBehaviour, IPickupable
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
        Debug.Log("Armour pickup obtained");
        Destroy(this.gameObject);
    }

    void Update()
    {
        
    }
}
