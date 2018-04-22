using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Weapons;

namespace TheSquad.AttackSystem
{
    public abstract class AbstractAttack : NetworkBehaviour
    {
        AbstractWeapon weapon;

        protected virtual void Attack()
        {
            weapon = GetComponentInChildren<AbstractWeapon>();

            if(weapon != null) weapon.Fire();
        }
    }
}