using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TankCombat : MonoBehaviour
{
    [SerializeField] Transform firePoint = null;
    [SerializeField] GameObject projectile = null;

    [SerializeField] private int damage = 1;

    PhotonView view;

    void Start()
    {
        view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    /// <summary>
    /// Instantiates a projectice at the firepoint
    /// </summary>
    void Shoot()
    {
        if(SceneManager.GetActiveScene().name == "OfflineGame")
        {
            GameObject shell = Instantiate(projectile, firePoint.position, firePoint.rotation);
            shell.GetComponent<Bullet>().damage = damage;
        }
        else if(SceneManager.GetActiveScene().name == "OnlineGame")
        {
            GameObject shell = PhotonNetwork.Instantiate(projectile.name, firePoint.position, firePoint.rotation);
            shell.GetComponent<Bullet>().damage = damage;
        }
    }
}
