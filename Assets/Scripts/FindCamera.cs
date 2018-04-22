using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FindCamera : NetworkBehaviour
{
    [SerializeField] GameObject cameraLocation;

    void Start()
    {
        if(isLocalPlayer) CameraController.refCam.FollowPlayer(cameraLocation);
    }
}