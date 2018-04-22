
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public abstract class AbstactMovement : NetworkBehaviour
{
    protected abstract void Move();
}