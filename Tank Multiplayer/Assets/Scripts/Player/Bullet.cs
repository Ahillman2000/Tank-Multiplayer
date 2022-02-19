using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject tank;
    Tank tankStats;

    [SerializeField] float speed = 10f;
    Rigidbody2D rb;
    //[SerializeField] private int damage = 1;

    void Start()
    {
        tankStats = tank.GetComponent<Tank>();

        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log(hit.name);

        if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
        {
            //Debug.Log("hit tank");

            Tank hitTankStats = hit.GetComponent<Tank>();
            hitTankStats.TakeDamage(tankStats.damage);

            if (hitTankStats.GetLives() == 0)
            {
                Debug.Log("Player killed");
                tankStats.IncreaseScore();
            }
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
