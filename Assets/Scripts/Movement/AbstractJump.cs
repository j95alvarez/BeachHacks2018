
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public abstract class AbstractJump : NetworkBehaviour {
    protected abstract void Jump();
}
