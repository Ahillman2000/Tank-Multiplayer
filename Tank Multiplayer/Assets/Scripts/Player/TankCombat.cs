using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TankCombat : MonoBehaviour
{
    [SerializeField] Transform firePoint = null;
    [SerializeField] GameObject projectile = null;

    PhotonView view;

    void Start()
    {
        view = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
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

    void Shoot()
    {
        if(SceneManager.GetActiveScene().name == "OfflineGame")
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
        else if(SceneManager.GetActiveScene().name == "OnlineGame")
        {
            PhotonNetwork.Instantiate(projectile.name, firePoint.position, firePoint.rotation);
        }
    }
}
