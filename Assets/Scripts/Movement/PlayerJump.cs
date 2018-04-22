using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : AbstractJump {
    [SerializeField]
    private int JumpForce;
    [SerializeField]
    private int gravity;

    private bool isGrounded = true;
    Rigidbody rb;
    

    private void FixedUpdate() {
        Jump();
    }

    protected override void Jump() {
        var zMovement = Input.GetKeyDown("space");
        rb = GetComponent<Rigidbody>();

        if(isGrounded && zMovement) {
            rb.AddForce(Vector3.up*JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }
}

