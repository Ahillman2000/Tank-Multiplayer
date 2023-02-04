using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObj = collision.gameObject;
        if (hitObj.GetComponent<IDamagable>() != null)
        {
            IDamagable damagable = hitObj.GetComponent<IDamagable>();
            damagable.Damage(owner, damage);
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
