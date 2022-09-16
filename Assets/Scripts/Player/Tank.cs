using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Tank : MonoBehaviour
{
    public int max_lives = 3;
    private int current_lives;

    public int damage = 1;

    [SerializeField] GameObject deathEffect = null;

    private static int score = 0;

    public float speed = 3f;

    void Start()
    {
        current_lives = max_lives;
    }

    public int GetLives()
    {
        return current_lives;
    }

    public void TakeDamage(int _damage)
    {
        current_lives -= _damage;

        if (current_lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (SceneManager.GetActiveScene().name == "OfflineGame")
        {
            if (this.CompareTag("Player"))
            {
                Debug.Log("GAME OVER");
            }

            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
        }
        else if (SceneManager.GetActiveScene().name == "OnlineGame")
        {
            PhotonNetwork.Instantiate(deathEffect.name, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);

        //Respawn();
    }

    void Respawn()
    {
        current_lives = max_lives;
    }

    public void IncreaseScore()
    {
        score ++;
        Debug.Log("player Score: " + score);
    }

    public void Debugger()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    void Update()
    {
        //Debug.Log("player score: " + score);
        Debugger();
    }
}
