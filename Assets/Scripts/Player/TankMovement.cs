using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankMovement : MonoBehaviour
{
    Tank tank;
    Rigidbody2D rb;

    float movementInput;

    float rotationInput;
    Vector3 eulerAngleVelocity;
    [SerializeField] float tankRotationSpeed = 100f;

    public float movementSpeed = 3f;

    PhotonView view;

    void Start()
    {
        tank = this.GetComponent<Tank>();

        rb = this.GetComponent<Rigidbody2D>();
        view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine)
        {
            movementInput = Input.GetAxisRaw("Vertical");

            rotationInput = Input.GetAxisRaw("Horizontal");
            eulerAngleVelocity = new Vector3(0, 0, rotationInput * tankRotationSpeed * -1);
        }
    }

    private void FixedUpdate()
    {
        if(view.IsMine)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.transform.rotation * deltaRotation);

            //rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);
            rb.MovePosition(this.transform.position + movementInput * movementSpeed * Time.fixedDeltaTime * transform.up);
        }
    }
}
