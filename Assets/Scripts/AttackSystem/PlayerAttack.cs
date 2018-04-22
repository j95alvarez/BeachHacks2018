using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TheSquad.Weapons;

namespace TheSquad.AttackSystem
{
    public class PlayerAttack : AbstractAttack
    {
        void FixedUpdate()
        {
            if (!isLocalPlayer) { return; }

            if (Input.GetKeyDown(KeyCode.Space)) Attack();
        }
    }
}