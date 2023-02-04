﻿using System.Collections;
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

    [SerializeField] private HealthUI healthUI;

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
            attacker.IncreaseScore(1);
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
                UIManager.Instance.GameOver();
            }
        }
        else if (SceneManager.GetActiveScene().name == "OnlineGame")
        {
            PhotonNetwork.Instantiate(deathEffect.name, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
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
    }

    /// <summary>
    /// Debug functionality for the object
    /// </summary>
    public void Debugger()
    {
        if (this.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                Damage(this, 1);
            }
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                IncreaseScore(1);
            }
        }
    }

    void Update()
    {
        Debugger();
    }
}
