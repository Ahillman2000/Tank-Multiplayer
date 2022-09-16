using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretControl : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;

    public Rigidbody2D tankRigidBody;

    PhotonView view;

    private void Start()
    {
        mainCam = Camera.main;
        view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if(view.IsMine)
        {
            Vector2 lookDir = mousePos - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            this.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
