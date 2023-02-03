using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody2D rb;
    public Tank owner;
    
    public int damage;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<IDamagable>() != null)
        {
            IDamagable hitObject = hit.GetComponent<IDamagable>();
            hitObject.Damage(owner, damage);
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
