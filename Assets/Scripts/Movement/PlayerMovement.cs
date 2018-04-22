
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstactMovement
{
    [SerializeField] float speed;
    [SerializeField] float aimSpeed;

    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        if (!isLocalPlayer) { return; }

        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * aimSpeed;
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);
    }
}