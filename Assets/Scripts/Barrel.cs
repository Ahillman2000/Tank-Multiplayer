using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 1;
    public int MaxHealth => maxHealth;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField] private GameObject DestructionEffect;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destory();
        }
    }

    public void Destory()
    {
        if (SceneManager.GetActiveScene().name == "OfflineGame")
        {
            if (this.CompareTag("Player"))
            {
                Debug.Log("GAME OVER");
            }

            Instantiate(DestructionEffect, this.transform.position, this.transform.rotation);
        }
        else if (SceneManager.GetActiveScene().name == "OnlineGame")
        {
            PhotonNetwork.Instantiate(DestructionEffect.name, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
