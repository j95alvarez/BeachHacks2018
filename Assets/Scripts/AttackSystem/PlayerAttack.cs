using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Weapons;

namespace TheSquad.AttackSystem
{
    public class PlayerAttack : AbstractAttack
    {
        void FixedUpdate()
        {
            if (!isLocalPlayer) { return; }
            if (Input.GetButtonDown("Fire1")) Attack();
        }
    }
}