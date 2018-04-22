using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    public static CameraController refCam;

    GameObject player;
    Camera cam;
    bool following;

    void Start()
    {
        refCam = this;
        cam = GetComponent<Camera>();
        following = false;
    }

    void Update()
    {
        if(following)
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
    }

    public void FollowPlayer(GameObject p)
    {
        player = p;
        following = true;
    }
}