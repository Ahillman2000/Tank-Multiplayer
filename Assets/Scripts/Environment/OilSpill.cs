using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField] private float slowDownMultipler = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<TankMovement>() != null)
        {
            collision.gameObject.GetComponent<TankMovement>().movementSpeed *= slowDownMultipler;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TankMovement>() != null)
        {
            collision.gameObject.GetComponent<TankMovement>().movementSpeed /= slowDownMultipler;
        }
    }
}
