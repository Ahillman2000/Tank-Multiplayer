using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankMovement : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 eulerAngleVelocity;
    [SerializeField] float tankRotationSpeed = 100f;

    public float movementSpeed = 3f;

    PhotonView view;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine)
        {
            eulerAngleVelocity = new Vector3(0, 0, InputManager.Instance.Rotation * tankRotationSpeed * -1);
        }
    }

    private void FixedUpdate()
    {
        if(view.IsMine)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.transform.rotation * deltaRotation);

            rb.MovePosition(this.transform.position + InputManager.Instance.Movemement * movementSpeed * Time.fixedDeltaTime * transform.up);
        }
    }
}
