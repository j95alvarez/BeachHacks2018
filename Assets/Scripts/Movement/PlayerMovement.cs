
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstactMovement
{
    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);
    }
}