using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Tank : MonoBehaviour, IDamagable
{
    public int maxLives = 3;
    public int MaxHealth => maxLives;

    [SerializeField] private int currentLives;
    public int CurrentHealth => currentLives;

    [SerializeField] private GameObject deathEffect = null;

    public int score = 0;

    void Start()
    {
        currentLives = maxLives;
    }

    /// <summary>
    /// Reduces the health by a set amount
    /// </summary>
    /// <param name="damage"> Amount of health to be reduced by </param>
    public void Damage(Tank attacker, int damage)
    {
        currentLives -= damage;

        if (currentLives <= 0)
        {
            attacker.score++;
            Destory();
        }
    }

    public void Repair(int health)
    {
        if(currentLives < maxLives)
        {
            currentLives += health;
            Debug.Log("Unit healed", gameObject);
        }
    }

    /// <summary>
    /// Called when health reaches 0
    /// </summary>
    public void Destory()
    {
        if (SceneManager.GetActiveScene().name == "OfflineGame")
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);

            if (this.CompareTag("Player"))
            {
                Debug.Log("GAME OVER");
            }
        }
        else if (SceneManager.GetActiveScene().name == "OnlineGame")
        {
            PhotonNetwork.Instantiate(deathEffect.name, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);

        //Respawn();
    }

    /// <summary>
    /// Respawns the object
    /// </summary>
    void Respawn()
    {
        currentLives = maxLives;
    }

    /// <summary>
    /// Increments the objects score by a set amount
    /// </summary>
    public void IncreaseScore(int increment)
    {
        score += increment;
        Debug.Log("player Score: " + score, gameObject);
    }

    /// <summary>
    /// Debug functionality for the object
    /// </summary>
    public void Debugger()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Damage(this, 1);
        }
    }

    void Update()
    {
        //Debug.Log("player score: " + score);
        Debugger();
    }
}
