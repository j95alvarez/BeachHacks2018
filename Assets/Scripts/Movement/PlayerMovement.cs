
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstactMovement
{
    [SerializeField] float speed;
    [SerializeField] float aimSpeed;
    public bool pressDown = false;

    void FixedUpdate()
    {
        Move();
    }

    protected override void Move() {
        if (!isLocalPlayer) { return; }

        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * aimSpeed;
        var vertical = 0.0f;

        if (Input.GetKey(KeyCode.LeftShift)) {
            vertical = Input.GetAxis("Vertical") * Time.deltaTime * (speed/2);
            pressDown = true;
        }
        else {
            vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            pressDown = false;
        }

        transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);
    }
}